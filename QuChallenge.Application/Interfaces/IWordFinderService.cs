using QuChallenge.Application.Dtos;

namespace QuChallenge.Application.Interfaces
{
    /// <summary>
    /// Defines the operations for processing word searches.
    /// </summary>
    public interface IWordFinderService
    {
        /// <summary>
        /// Processes a matrix and a word stream to find matching words.
        /// </summary>
        /// <param name="request">The request with the matrix and word stream values.</param>
        /// <returns>A list of found words.</returns>
        IEnumerable<string> FindWords(WordFinderRequestDto request);
    }
}

