namespace MangaList.Application.DTOs;
public record MangaDetailsRequest(
    int? Rank,
    string Url,
    string Title,
    string? Status,
    string? Published,
    int? Volumes,
    double? Score,
    string Authors,
    string? Synopsis,
    string? ImgUrl
);