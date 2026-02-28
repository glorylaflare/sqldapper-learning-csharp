namespace Todo.Api.Models;

public class Entity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
