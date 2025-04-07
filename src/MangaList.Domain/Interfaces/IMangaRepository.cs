using MangaList.Domain.Entities;

namespace MangaList.Domain.Interfaces;

/* The code snippet defines a C# interface named `IMangaRepository`. This interface declares three
asynchronous methods related to manga entities: */
public interface IMangaRepository
{
    // Method to show mangá details like synopsis and volumes.
    Task<Manga?> GetMangaByIdAsync(Guid Id);
    Task<MangaCollection> GetMangaAsync(int page, string? key, string? order, string search);
    Task UpsertMangaAsync(List<Manga> mangas);
}
