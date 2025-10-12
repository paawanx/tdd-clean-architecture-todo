using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using ToDoApp.Application.Interfaces;
using ToDoApp.Core;

namespace ToDoApp.Tests;

public class CreateTodoApiTests(WebApplicationFactory<TestEntryPoint> factory) : IClassFixture<WebApplicationFactory<TestEntryPoint>>
{
    [Fact]
    public async Task POST_todos_ReturnsCreateAndPersists_WhenTitleValid()
    {
        // Arrange
        var mockRepo = new Mock<ITodoRepository>();
        Todo? captured = null;
        mockRepo.Setup(r => r.AddAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .Callback<Todo, CancellationToken>((t, ct) => captured = t)
            .Returns(Task.CompletedTask);

        var client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => mockRepo.Object);
            });
        }).CreateClient();

        var dto = new { Title = "Buy paper" };
        
        // Act
        var response = await client.PostAsJsonAsync("/todos", dto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        captured.Should().NotBeNull();
        captured!.Title.Should().Be("Buy paper");
    }
}