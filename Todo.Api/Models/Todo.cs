namespace Todo.Api.Models;

public class Todo : Entity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required bool IsDone { get; set; }
}