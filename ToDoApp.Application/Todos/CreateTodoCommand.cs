using MediatR;
using ToDoApp.Core;

namespace ToDoApp.Application.Todos;

public record CreateTodoCommand(string Title) : IRequest<Result<Guid>>;