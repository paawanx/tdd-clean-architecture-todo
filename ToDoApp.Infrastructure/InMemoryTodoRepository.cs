using System.Collections.Concurrent;
using ToDoApp.Application.Interfaces;
using ToDoApp.Core;

namespace ToDoApp.Infrastructure;

public class InMemoryTodoRepository: ITodoRepository
{
    private readonly ConcurrentDictionary<Guid, Todo> _store = new();

    public Task AddAsync(Todo todo, CancellationToken cancellationToken)
    {
        _store[todo.Id] = todo;
        return Task.CompletedTask;
    }
}