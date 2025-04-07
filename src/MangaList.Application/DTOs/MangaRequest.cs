namespace MangaList.Application.DTOs;
public class MangaRequest
{
    public string? ImgUrl { get; set; }
    public string? Title { get; set; }
    public string? Status { get; set; }
    public int? Volumes { get; set; }
    public string? Url { get; set; }
};