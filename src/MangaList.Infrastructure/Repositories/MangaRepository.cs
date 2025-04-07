using Microsoft.EntityFrameworkCore;
using MangaList.Domain.Entities;
using MangaList.Domain.Interfaces;
using MangaList.Infrastructure.DTOs;
using MangaList.Infrastructure.Persistence;

namespace MangaList.Infrastructure.Repositories;
public class MangaRepository : IMangaRepository
{
    public readonly MangaDbContext _context;

    public MangaRepository(MangaDbContext context)
    {
        _context = context;
    }

    // TODO: Add pageSize as a parameter
    public async Task<MangaCollection> GetMangaAsync(int page, string? key, string? order, string search)
    {
        var query = _context.Mangas.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query
                        .Where(m =>
                            !string.IsNullOrEmpty(m.Title) &&
                            m.Title.ToLower().Contains(search.ToLower()));
        }

        if (!string.IsNullOrEmpty(key))
        {
            if (key.ToLower() == "title")
            {
                query = order.ToLower() == "asc"
                    ? query.OrderBy(m => m.Title)
                    : query.OrderByDescending(m => m.Title);

            }
        }

        const int pageSize = 25;

        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        var mangas = await query.ToListAsync();
        int total = await _context.Mangas.CountAsync();

        return new MangaCollection
        {
            Mangas = mangas ?? new List<Manga>(),
            MangasTotal = total
        };
    }

    public async Task<Manga?> GetMangaByIdAsync(Guid Id)
    {
        return await _context.Mangas
            .Where(m => m.Id == Id)
            .SingleOrDefaultAsync();
    }

    public async Task UpsertMangaAsync(List<Manga> mangas)
    {
        foreach (var manga in mangas)
        {
            var existingManga = await _context.Mangas
                .Where(m => m.Url == manga.Url)
                .FirstOrDefaultAsync();

            // Update manga if exists in database
            if (existingManga != null)
            {
                existingManga.Rank = manga.Rank;
                existingManga.Title = manga.Title;
                existingManga.Status = manga.Status;
                existingManga.Published = manga.Published;
                existingManga.Volumes = manga.Volumes;
                existingManga.Score = manga.Score;
                existingManga.Authors = manga.Authors;
                existingManga.Synopsis = manga.Synopsis;
                existingManga.ImgUrl = manga.ImgUrl;
                existingManga.CreatedAt = existingManga.CreatedAt; // Keep the created datetime (the first insertion in database) 

                _context.Mangas.Update(existingManga);
            }
            // Create new manga registre in database
            else
            {
                await _context.Mangas.AddAsync(manga);
            }
        }

        await _context.SaveChangesAsync();
    }
}