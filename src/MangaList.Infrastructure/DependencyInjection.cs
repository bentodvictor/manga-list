using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MangaList.Domain.Interfaces;
using MangaList.Infrastructure.Persistence;
using MangaList.Infrastructure.Repositories;

namespace MangaList.Infrastructure;

/* The DependencyInjection class provides a method to add infrastructure services to the
IServiceCollection in C#. */
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<MangaDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<IMangaRepository, MangaRepository>();

        return services;
    }
}