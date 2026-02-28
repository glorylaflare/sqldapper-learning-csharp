namespace Todo.Api.Dtos;

public record class TodoDto(string Title, string Description, bool IsDone);