namespace Todo.Api.Interfaces;

public interface ITodoRepository
{
    Task<int> AddAsync(TodoDto todo);
    Task<IEnumerable<Models.Todo>> GetAllAsync();
    Task<Models.Todo>? GetByIdAsync(int id);
    Task<IEnumerable<Models.Todo>> GetByCondition(bool isDone);
    Task<int> UpdateAsync(int id, TodoDto todo);
    Task<int> DeleteAsync(int id);
}
