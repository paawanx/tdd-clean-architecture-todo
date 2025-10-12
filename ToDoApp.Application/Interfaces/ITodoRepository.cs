using ToDoApp.Core;

namespace ToDoApp.Application.Interfaces;

public interface ITodoRepository
{
    Task AddAsync(Todo todo, CancellationToken cancellationToken);
}