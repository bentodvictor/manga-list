using MangaList.Infrastructure.Jobs;
using MangaList.Domain.Interfaces;  // Ensure you include this for IServiceProvider

namespace MangaList.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Log the worker status
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                // Create a scope to resolve scoped dependencies
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Resolve scoped services within the scope
                    var mangaUpsertJob = scope.ServiceProvider.GetRequiredService<MangaUpsertJob>();
                    var mangaRepository = scope.ServiceProvider.GetRequiredService<IMangaRepository>();

                    // Perform the upsert job (using mangaUpsertJob)
                    await mangaUpsertJob.UpsertMangasAsync();
                }

                // Delay before running again
                await Task.Delay(60 * 60 * 1000, stoppingToken);  // Delay for 60 minutes
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the job.");
            }
        }
    }
}
