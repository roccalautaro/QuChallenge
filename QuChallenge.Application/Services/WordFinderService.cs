using QuChallenge.Application.Dtos;
using QuChallenge.Application.Interfaces;
using QuChallenge.Domain.Entities;

namespace QuChallenge.Application.Services;

/// <summary>
/// Service for processing word searches in a matrix.
/// </summary>
public class WordFinderService : IWordFinderService
{
    /// <inheritdoc/>
    public IEnumerable<string> FindWords(WordFinderRequestDto request)
    {
        try
        {
            var wordFinder = new WordFinder(request.Matrix!);
            return wordFinder.Find(request.WordStream!);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while processing the word search.", ex);
        }
    }
}
