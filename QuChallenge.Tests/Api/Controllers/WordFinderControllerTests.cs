using Microsoft.AspNetCore.Mvc;
using Moq;
using QuChallenge.Api.Controllers;
using QuChallenge.Application.Dtos;
using QuChallenge.Application.Interfaces;

namespace QuChallenge.Tests.Api.Controllers;

public class WordFinderControllerTests
{
    private readonly Mock<IWordFinderService> _wordFinderServiceMock;
    private readonly WordFinderController _controller;

    public WordFinderControllerTests()
    {
        _wordFinderServiceMock = new Mock<IWordFinderService>();
        _controller = new WordFinderController(_wordFinderServiceMock.Object);
    }

    [Fact]
    public void FindWords_ShouldReturnOk_WhenWordsAreFound()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = new List<string> { "chill", "coldw", "windd" },
            WordStream = new List<string> { "chill", "cold", "wind" }
        };
        var expectedWords = new List<string> { "chill", "cold", "wind" };

        _wordFinderServiceMock
            .Setup(service => service.FindWords(request))
            .Returns(expectedWords);

        // Act
        var result = _controller.FindWords(request) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedWords, result.Value);
    }

    [Fact]
    public void FindWords_ShouldReturnBadRequest_WhenMatrixIsEmpty()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = new List<string>(),
            WordStream = new List<string> { "chill" }
        };

        // Act
        var result = _controller.FindWords(request) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Matrix and word stream cannot be null.", result.Value);
    }

    [Fact]
    public void FindWords_ShouldReturnBadRequest_WhenMatrixExceeds64x64()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = Enumerable.Repeat("a".PadRight(65, 'a'), 65).ToList(),
            WordStream = new List<string> { "chill" }
        };

        // Act
        var result = _controller.FindWords(request) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Matrix size must not exceed 64x64.", result.Value);
    }

    [Fact]
    public void FindWords_ShouldReturnBadRequest_WhenWordStreamIsEmpty()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = new List<string> { "chill", "coldw", "windd" },
            WordStream = new List<string>()
        };

        // Act
        var result = _controller.FindWords(request) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Matrix and word stream cannot be null.", result.Value);
    }

    [Fact]
    public void FindWords_ShouldReturnNotFound_WhenNoWordsAreFound()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = new List<string> { "aaaa", "bbbb", "cccc" },
            WordStream = new List<string> { "chill", "cold" }
        };

        _wordFinderServiceMock
            .Setup(service => service.FindWords(request))
            .Returns(new List<string>());

        // Act
        var result = _controller.FindWords(request) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("No words were found in the matrix.", result.Value);
    }

    [Fact]
    public void FindWords_ShouldReturnBadRequest_WhenRequestDataIsNull()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = null,
            WordStream = null
        };

        // Act
        var result = _controller.FindWords(request) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Matrix and word stream cannot be null.", result.Value);
    }

    [Fact]
    public void FindWords_ShouldReturnBadRequest_WhenMatrixIsNull()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = null,
            WordStream = new List<string> { "chill", "cold" }
        };

        // Act
        var result = _controller.FindWords(request) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Matrix and word stream cannot be null.", result.Value);
    }


}

