using MediatR;
using ToDoApp.Application.Interfaces;
using ToDoApp.Core;

namespace ToDoApp.Application.Todos;

public class CreateTodoCommandHandler(ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, Result<Guid>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public Task<Result<Guid>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        // placeholder for now (always succeeds)
        var todo = Todo.Create(request.Title);
        return Task.FromResult(Result<Guid>.Success(Guid.NewGuid()));
    }
}