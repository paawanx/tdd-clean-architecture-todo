using Xunit;
using FluentAssertions;
using ToDoApp.Core;

namespace ToDoApp.Tests
{
    
    public class TodoAppTests
    {
        [Fact]
        public void Create_ShouldFail_WhenTitleIsEmpty()
        {
            // Arrange
            var title = "";

            // Act
            var result = Todo.Create(title);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Title cannot be empty");
        }

        [Fact]
        public void Create_ShouldSucceed_WhenTitleIsValid()
        {
            // Arrange
            var title = "Buy milk";
            
            // Act
            var result = Todo.Create(title);
            
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.Title.Should().Be(title);
        }
    }

}