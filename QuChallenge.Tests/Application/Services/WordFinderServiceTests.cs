using QuChallenge.Application.Services;
using QuChallenge.Application.Dtos;

namespace QuChallenge.Tests.Services;

public class WordFinderServiceTests
{
    private readonly WordFinderService _service;

    public WordFinderServiceTests()
    {
        _service = new WordFinderService();
    }

    [Fact]
    public void FindWords_ShouldReturnMatchingWords_WhenInputIsValid()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = new List<string> { "chill", "coldw", "windd" },
            WordStream = new List<string> { "chill", "cold", "wind", "fire" }
        };

        // Act
        var result = _service.FindWords(request);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("chill", result);
        Assert.Contains("cold", result);
        Assert.Contains("wind", result);
        Assert.DoesNotContain("fire", result);
    }

    [Fact]
    public void FindWords_ShouldThrowInvalidOperationException_WhenUnexpectedErrorOccurs()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = null,
            WordStream = null
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _service.FindWords(request));
        Assert.Equal("An error occurred while processing the word search.", exception.Message);
    }

    [Fact]
    public void FindWords_ShouldHandleDuplicateWords_InWordStream()
    {
        // Arrange
        var request = new WordFinderRequestDto
        {
            Matrix = new List<string> { "chill", "coldw", "windd" },
            WordStream = new List<string> { "chill", "cold", "chill", "cold" }
        };

        // Act
        var result = _service.FindWords(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); // "chill" and "cold" should appear once each
        Assert.Contains("chill", result);
        Assert.Contains("cold", result);
    }
}
