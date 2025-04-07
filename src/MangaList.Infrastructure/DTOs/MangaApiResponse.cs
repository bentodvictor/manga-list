using System.Text.Json.Serialization;

namespace MangaList.Infrastructure.DTOs;

public class MangaApiResponse
{

    [JsonPropertyName("mal_id")]
    public int? MalId { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("images")]
    public Images? Images { get; set; }

    [JsonPropertyName("approved")]
    public bool Approved { get; set; }

    [JsonPropertyName("titles")]
    public List<Title>? Titles { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("title_english")]
    public string? TitleEnglish { get; set; }

    [JsonPropertyName("title_japanese")]
    public string? TitleJapanese { get; set; }

    [JsonPropertyName("title_synonyms")]
    public List<string>? TitleSynonyms { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("chapters")]
    public int? Chapters { get; set; }

    [JsonPropertyName("volumes")]
    public int? Volumes { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("publishing")]
    public bool Publishing { get; set; }

    [JsonPropertyName("published")]
    public Published? Published { get; set; }

    [JsonPropertyName("score")]
    public double? Score { get; set; }

    [JsonPropertyName("scored_by")]
    public int? ScoredBy { get; set; }

    [JsonPropertyName("rank")]
    public int? Rank { get; set; }

    [JsonPropertyName("popularity")]
    public int? Popularity { get; set; }

    [JsonPropertyName("members")]
    public int? Members { get; set; }

    [JsonPropertyName("favorites")]
    public int? Favorites { get; set; }

    [JsonPropertyName("synopsis")]
    public string? Synopsis { get; set; }

    [JsonPropertyName("background")]
    public string? Background { get; set; }

    [JsonPropertyName("authors")]
    public List<Author>? Authors { get; set; }

    [JsonPropertyName("serializations")]
    public List<Serialization>? Serializations { get; set; }

    [JsonPropertyName("genres")]
    public List<Genre>? Genres { get; set; }

    [JsonPropertyName("demographics")]
    public List<Demographic>? Demographics { get; set; }
}

public class Images
{
    [JsonPropertyName("jpg")]
    public ImageDetails? Jpg { get; set; }

    [JsonPropertyName("webp")]
    public ImageDetails? Webp { get; set; }
}

public class ImageDetails
{
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("small_image_url")]
    public string? SmallImageUrl { get; set; }

    [JsonPropertyName("large_image_url")]
    public string? LargeImageUrl { get; set; }
}

public class Title
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("title")]
    public string? Name { get; set; }
}

public class Published
{
    [JsonPropertyName("from")]
    public DateTime? From { get; set; }

    [JsonPropertyName("to")]
    public DateTime? To { get; set; }

    [JsonPropertyName("string")]
    public string? DateString { get; set; }
}

public class Author
{
    [JsonPropertyName("mal_id")]
    public int? MalId { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

public class Serialization
{
    [JsonPropertyName("mal_id")]
    public int? MalId { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

public class Genre
{
    [JsonPropertyName("mal_id")]
    public int? MalId { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

public class Demographic
{
    [JsonPropertyName("mal_id")]
    public int? MalId { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}