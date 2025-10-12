using Moq;
using FluentAssertions;

using ToDoApp.Application.Todos;
using ToDoApp.Application.Interfaces;
using ToDoApp.Core;

namespace ToDoApp.Tests
{
    public class CreateTodoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldFail_WhenTitleIsEmpty()
        {
            // Arrange
            var command = new CreateTodoCommand(" ");

            var mockRepo = new Mock<ITodoRepository>();
            // handler will use repo only o success; with invalid input repo should not be called

            var handler = new CreateTodoCommandHandler(mockRepo.Object);
            
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            
            // Assert - validation failure
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Title cannot be empty");
            
            // Repo should never be called when validation fails
            mockRepo.Verify(r => r.AddAsync(It.IsAny<ToDoApp.Core.Todo>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturn_TodoId_WhenTitleIsValid()
        {
            // Arrange
            var title = "Buy cheese";
            var command = new CreateTodoCommand(title);

            Todo? capturedTodo = null;

            var mockRepo = new Mock<ITodoRepository>();
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
                .Callback<Todo, CancellationToken>((t, ct) => capturedTodo = t)
                .Returns(Task.CompletedTask);

            var handler = new CreateTodoCommandHandler(mockRepo.Object);
            
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
            
            mockRepo.Verify(r=>r.AddAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()), Times.Once);

            capturedTodo.Should().NotBeNull("repository should recieve the created token");
            result.Value.Should().Be(capturedTodo!.Id);
        }
    }
}