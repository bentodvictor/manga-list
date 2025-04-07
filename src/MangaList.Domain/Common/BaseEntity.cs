namespace MangaList.Domain.Common;

/* The BaseEntity class defines properties for an entity with an Id, CreatedAt, and UpdatedAt
timestamps. */
public class BaseEntity
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
}