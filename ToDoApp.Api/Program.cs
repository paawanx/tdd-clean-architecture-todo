using MediatR;

using ToDoApp.Application.Todos;
using ToDoApp.Application.Interfaces;
using ToDoApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTodoCommand).Assembly)
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register concrete repository
builder.Services.AddScoped<ITodoRepository, InMemoryTodoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/todos", async (CreateTodoDto dto, IMediator mediator) =>
{
    var command = new CreateTodoCommand(dto.Title);
    var result = await mediator.Send(command);
    return result.IsSuccess
        ? Results.Created($"/todos/{result.Value}", new { id = result.Value })
        : Results.BadRequest(new { error = result.Error });
});

app.Run();

public record CreateTodoDto(string Title);
