using Hangfire;
using MangaList.Domain.Interfaces;
using MangaList.Infrastructure.Jobs;
using MangaList.Infrastructure.Repositories;
using MangaList.Worker;
using MangaList.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
// Register IDistributedCache with InMemoryCache (for development)
// You can change this to another implementation like Redis or SQL Server if needed
builder.Services.AddDistributedMemoryCache();  // This registers the in-memory cache implementation

// Register the DbContext with a connection string for SQL Server (make sure to replace this with your connection string)
builder.Services.AddDbContext<MangaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register Hangfire, your services, and other dependencies...
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<MangaUpsertJob>();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<Worker>();
builder.Logging.AddConsole();  // This ensures logs will be written to the terminal

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<MangaDbContext>();
  dbContext.Database.EnsureCreated();
}

host.Run();
