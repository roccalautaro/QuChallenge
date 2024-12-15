using QuChallenge.Domain.Entities;

namespace QuChallenge.Tests.Domain;

public class WordFinderTests
{
    [Fact]
    public void IgnoreDuplicatedWords()
    {
        // Arrange
        var matrix = new List<string>
        {
            "hellohellohellohello",
            "worldworldworldworld",
            "testtesttesttesttest",
            "rainbowrainbowrainbo",
            "foxfoxfoxfoxfoxfoxfo"
        };

        var wordStream = new List<string> { "hello", "hello", "world", "world" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("hello", result);
        Assert.Contains("world", result);
        Assert.Equal(2, result.Count()); 
    }

    [Fact]
    public void OrderWordsByFrequencyInCols()
    {
        // Arrange
        var matrix = new List<string>
        {
            "tumtut",
            "osioso",
            "paxpap"
        };

        var wordStream = new List<string> { "top", "usa", "mix" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream).ToList();

        // Assert
        Assert.Equal("top", result[0]); 
        Assert.Equal("usa", result[1]); 
        Assert.Equal("mix", result[2]); 
    }

    [Fact]
    public void OrderWordsByFrequencyInRows()
    {
        // Arrange
        var matrix = new List<string>
        {
            "hellohelloworldworld",
            "hellohelloworldworld",
            "hellotestworldrainbo"
        };

        var wordStream = new List<string> { "hello", "world", "test", "rainbow" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream).ToList();

        // Assert
        Assert.Equal("hello", result[0]); 
        Assert.Equal("world", result[1]); 
        Assert.Equal("test", result[2]);
    }

    [Fact]
    public void ReturnEmptyWhenNoWordsFound()
    {
        // Arrange
        var matrix = new List<string>
        {
            "abcdefghijabcdefghij",
            "klmnopqrstklmnopqrst",
            "uvwxyzabcdefuvwxyzab"
        };

        var wordStream = new List<string> { "missing", "notfound" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ReturnMatchingWordsWhenFoundInCol()
    {
        // Arrange
        var matrix = new List<string>
        {
            "hxxxx",
            "exxxx",
            "lxxxx",
            "lxxxx",
            "oxxxx"
        };

        var wordStream = new List<string> { "hello" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.Contains("hello", result);
    }

    [Fact]
    public void ReturnMatchingWordsWhenFoundInRow()
    {
        // Arrange
        var matrix = new List<string>
        {
            "helloworldxxxxxxx",
            "xxxxxxxhelloworld",
            "testmatrixxxxxxxx"
        };

        var wordStream = new List<string> { "hello", "world", "test" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.Contains("hello", result);
        Assert.Contains("world", result);
        Assert.Contains("test", result);
    }

    [Fact]
    public void ReturnTopTenWordsWhenMoreThan10WordsFound()
    {
        // Arrange
        var matrix = new List<string>
        {
            "hellohello",
            "teststests",
            "worldworld",
            "sunoosunoo",
            "foxxxfoxxx",
            "birdqbirdq",
            "dataadataa",
            "rocksrocks",
            "catsscatss",
            "dogssdogss",
            "moonnasdfg",
            "treeeeeeee"
        };

        var wordStream = new List<string>
        {
            "hello", "world", "test", "fox", "tree", "bird",
            "rock", "cat", "dog", "sun", "moon", "data", "analysis"
        };

        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count());
        Assert.DoesNotContain("moon", result);
        Assert.DoesNotContain("tree", result);
    }


}
