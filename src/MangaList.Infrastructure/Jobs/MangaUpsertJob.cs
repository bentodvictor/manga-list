using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Polly;
using MangaList.Domain.Entities;
using MangaList.Domain.Interfaces;
using MangaList.Infrastructure.DTOs;

namespace MangaList.Infrastructure.Jobs;

public class MangaUpsertJob
{
    private readonly IMangaRepository _mangaRepository;
    private readonly HttpClient _httpClient;
    private readonly IAsyncPolicy _rateLimitPolicy;
    private const string ApiUrl = "https://api.jikan.moe/v4/manga";

    // Rate limits configurations
    private static readonly SemaphoreSlim RateLimitSemaphore = new SemaphoreSlim(1, 1);
    private static DateTime _lastRequestTime = DateTime.MinValue;
    private static int _requestsInCurrentMinute = 0;
    private const int MaxRequestsPerSecond = 3;
    private const int MaxRequestsPerMinute = 60;

    public MangaUpsertJob(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
        _httpClient = new HttpClient();

        // Rate limit configuration
        _rateLimitPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(1, _ => TimeSpan.FromMilliseconds(1000));
    }

    public async Task UpsertMangasAsync()
    {
        int currentPage = 1;
        bool hasNextPage = true;

        while (hasNextPage)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiUrl}?page={currentPage}");

            try
            {
                await ApplyRateLimitingAsync();
                await _rateLimitPolicy.ExecuteAsync(async () =>
                {
                    var response = await _httpClient.SendAsync(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.NotModified)
                    {
                        Console.WriteLine("No new updates.");
                        return;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error fetching data: {response.StatusCode}");
                        return;
                    }


                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

                    // Access the "data" array
                    if (jsonResponse.TryGetProperty("data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
                    {
                        var mangas = new List<Manga>();

                        // Enumerate over the array of mangas
                        foreach (var manga in dataElement.EnumerateArray())
                        {
                            if (manga.ToString() != null)
                            {
                                var mangaValue = JsonSerializer.Deserialize<MangaApiResponse>(manga, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                                List<string?> authors = mangaValue?
                                    .Authors?
                                    .Select(a => a.Name)?
                                    .ToList() ?? new List<string?>();

                                mangas.Add(new Manga
                                {
                                    Rank = mangaValue?.Rank ?? null,
                                    Url = mangaValue?.Url ?? null,
                                    Title = mangaValue?.TitleEnglish ?? mangaValue?.Title ?? mangaValue?.TitleJapanese,
                                    Status = mangaValue?.Status ?? null,
                                    Published = mangaValue?.Published?.DateString ?? null,
                                    Volumes = mangaValue?.Volumes,
                                    Score = mangaValue?.Score ?? null,
                                    Authors = authors.IsNullOrEmpty()
                                        ? string.Join(", ", authors)
                                        : null,
                                    Synopsis = mangaValue?.Synopsis,
                                    ImgUrl = mangaValue?.Images?.Jpg?.ImageUrl ?? string.Empty
                                });
                            }
                        }

                        // Now you can process the mangas
                        await _mangaRepository.UpsertMangaAsync(mangas);
                    }

                    // Handling pagination
                    hasNextPage = jsonResponse.GetProperty("pagination").GetProperty("has_next_page").GetBoolean();
                    currentPage++;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }
    }

    private async Task ApplyRateLimitingAsync()
    {
        await RateLimitSemaphore.WaitAsync();

        try
        {
            var now = DateTime.UtcNow;

            // Check if the per-minute limit has been exceeded
            if (_lastRequestTime.AddMinutes(1) < now)
            {
                _requestsInCurrentMinute = 0; // Reset every minute
            }

            // If the per-minute limit is reached, wait until the minute window resets
            if (_requestsInCurrentMinute >= MaxRequestsPerMinute)
            {
                var waitTime = _lastRequestTime.AddMinutes(1) - now;
                Console.WriteLine($"Rate limit exceeded per minute. Waiting {waitTime.TotalSeconds} seconds.");
                await Task.Delay(waitTime);
                _requestsInCurrentMinute = 0; // Reset count
            }

            // If the per-second limit is reached, wait until the second window resets
            if (_lastRequestTime.AddSeconds(1) > now)
            {
                var waitTime = _lastRequestTime.AddSeconds(1) - now;
                Console.WriteLine($"Rate limit exceeded per second. Waiting {waitTime.TotalMilliseconds} ms.");
                await Task.Delay(waitTime);
            }

            _lastRequestTime = DateTime.UtcNow;
            _requestsInCurrentMinute++;

        }
        finally
        {
            RateLimitSemaphore.Release();
        }
    }
}
