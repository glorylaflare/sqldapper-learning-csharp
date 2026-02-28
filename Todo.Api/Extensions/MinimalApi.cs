namespace Todo.Api.Extensions;

public static class MinimalApi
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/api/todos").WithTags(nameof(Todo));

        group.MapPost("/", async (TodoDto todo, ITodoRepository repo) =>
        {
            var rows = await repo.AddAsync(todo);

            return rows == -1 ? Results.BadRequest() : Results.Created();
        });

        group.MapGet("/{id:int}", async (int id, ITodoRepository repo) =>
        {
            #nullable disable
            var todo = await repo.GetByIdAsync(id);

            return todo is null ? Results.NotFound() : Results.Ok(todo);
        });

        group.MapGet("/notdone", async (bool isDone, ITodoRepository repo) =>
        {
            var todo = await repo.GetByCondition(isDone);

            return todo is null ? Results.NotFound() : Results.Ok(todo);
        });

        group.MapGet("/", async (ITodoRepository repo) =>
        {
            var todo = await repo.GetAllAsync();

            return todo is null ? Results.NotFound() : Results.Ok(todo);
        });

        group.MapDelete("/{id:int}", async (int id, ITodoRepository repo) =>
        {
            var rows = await repo.DeleteAsync(id);

            return rows == 0 ? Results.NotFound() : Results.NoContent();
        });

        group.MapPut("/{id:int}", async (int id, TodoDto todo, ITodoRepository repo) =>
        {
            var rows = await repo.UpdateAsync(id, todo);

            return rows < 1 ? Results.BadRequest() : Results.NoContent();
        });

        return group;
    }
}