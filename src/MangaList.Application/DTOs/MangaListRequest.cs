namespace MangaList.Application.DTOs;
public class MangaListRequest
{
    public List<MangaRequest>? Mangas { get; set; }
    public int MangasTotal { get; set; }
}