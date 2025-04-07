namespace MangaList.Domain.Entities;

/* The class MangaCollection represents a collection of manga with properties for the list of mangas
and the total number of mangas. */
public class MangaCollection
{
    public List<Manga> Mangas { get; set; } = new List<Manga>();
    public int MangasTotal { get; set; } = 0;
}