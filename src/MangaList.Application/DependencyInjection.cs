using Microsoft.Extensions.DependencyInjection;
using MangaList.Application.Mapper;
using MangaList.Application.Services;

namespace MangaList.Application;

/* The `DependencyInjection` class provides an extension method to
add application services to the `IServiceCollection` in C#. */
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MangaProfile));
        services.AddScoped<MangaService>();

        return services;
    }
}
