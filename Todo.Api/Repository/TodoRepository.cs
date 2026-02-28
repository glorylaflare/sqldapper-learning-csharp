namespace Todo.Api.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly IDbConnection _db;

    public TodoRepository(IDbConnection dbConnection) => _db = dbConnection;

    public async Task<int> AddAsync(TodoDto todo)
    {
        var sql = @"
            IF NOT EXISTS (SELECT 1 FROM Todos WHERE Title = @Title)
            BEGIN
                INSERT INTO Todos (Title, Description, IsDone)
                VALUES (@Title, @Description, @IsDone);

                SELECT CAST(SCOPE_IDENTITY() as int);
            END
            ELSE
            BEGIN
                SELECT Id FROM Todos WHERE Title = @Title;
            END";

        return await _db.ExecuteAsync(sql, todo);
    }

    public async Task<Models.Todo>? GetByIdAsync(int id)
    {
        var sql = @"
                SELECT * FROM Todos
                WHERE Id = @id";

        #nullable disable
        return await _db.QueryFirstOrDefaultAsync<Models.Todo>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Models.Todo>> GetAllAsync()
    {
        var sql = @"
                SELECT * FROM Todos
                ORDER BY CreatedAt DESC";

        return await _db.QueryAsync<Models.Todo>(sql);
    }

    public async Task<int> UpdateAsync(int id, TodoDto todo)
    {
        var sql = @"
                UPDATE Todos
                SET Title = @Title,
                    Description = @Description,
                    IsDone = @IsDone
                WHERE Id = @id";

        return await _db.ExecuteAsync(sql, new
        {
            Id = id,
            todo.Title,
            todo.Description,
            todo.IsDone,
            UpdatedAt = DateTime.UtcNow
        });
    }

    public async Task<int> DeleteAsync(int id)
    {
        var sql = @"
                DELETE FROM Todos
                WHERE Id = @id";

        return await _db.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<IEnumerable<Models.Todo>> GetByCondition(bool isDone)
    {
        var sql = @"
                SELECT * FROM Todos
                WHERE IsDone = @isDone";

        return await _db.QueryAsync<Models.Todo>(sql, new { isDone });
    }
}
