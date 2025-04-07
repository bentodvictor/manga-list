using Microsoft.EntityFrameworkCore;
using MangaList.Domain.Entities;

namespace MangaList.Infrastructure.Persistence;
public class MangaDbContext : DbContext
{
  public MangaDbContext(DbContextOptions<MangaDbContext> options) : base(options) { }

  public DbSet<Manga> Mangas { get; set; }
}
