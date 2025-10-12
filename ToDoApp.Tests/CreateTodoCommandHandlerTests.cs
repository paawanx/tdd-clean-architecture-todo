using Moq;
using ToDoApp.Application.Todos;
using ToDoApp.Application.Interfaces;
using FluentAssertions;

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
    }
}