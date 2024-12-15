namespace QuChallenge.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for a word finder.
    /// </summary>
    public interface IWordFinder
    {
        /// <summary>
        /// Finds words in a given word stream.
        /// </summary>
        /// <param name="wordStream">The word stream to search for.</param>
        /// <returns>A collection of found words.</returns>
        IEnumerable<string> Find(IEnumerable<string> wordStream);
    }
}
