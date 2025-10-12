using MediatR;
using ToDoApp.Application.Interfaces;
using ToDoApp.Core;

namespace ToDoApp.Application.Todos;

public class CreateTodoCommandHandler(ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, Result<Guid>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<Result<Guid>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var createResult = Todo.Create(request.Title);

        if (!createResult.IsSuccess)
        {
            return Result<Guid>.Failure(createResult.Error);
        }

        var todo = createResult.Value;
        await todoRepository.AddAsync(todo,cancellationToken);
        
        return Result<Guid>.Success(Guid.NewGuid());
    }
}