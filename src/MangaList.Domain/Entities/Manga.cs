using MangaList.Domain.Common;

namespace MangaList.Domain.Entities;

/* The `Manga` class in C# represents manga entities with properties such as rank, URL, title, status,
published date, volumes, score, and authors, along with a constructor method for initialization. */
public class Manga : BaseEntity
{
    public int? Rank { get; set; } = null;
    public string? Url { get; set; } = String.Empty;
    public string? Title { get; set; } = String.Empty;
    public string? Status { get; set; } = String.Empty;
    public string? Published { get; set; } = String.Empty;
    public int? Volumes { get; set; } = null;
    public double? Score { get; set; } = null;
    public string? Authors { get; set; } = String.Empty;
    public string? Synopsis { get; set; } = String.Empty;
    public string? ImgUrl { get; set; } = String.Empty;
}