using QuChallenge.Domain.Interfaces;

namespace QuChallenge.Domain.Entities;

/// <summary>
/// Implements the logic for finding words in a character matrix.
/// </summary>
public class WordFinder : IWordFinder
{
    private readonly char[,] _matrix;
    private readonly int _rows;
    private readonly int _cols;

    /// <summary>
    /// Initializes a new instance of the <see cref="WordFinder"/> class with a matrix.
    /// </summary>
    /// <param name="matrix">A collection of strings representing the character matrix.</param>
    public WordFinder(IEnumerable<string> matrix)
    {
        var matrixList = matrix.Select(row=> row.ToLower()).ToList();
        _rows = matrixList.Count;
        _cols = matrixList[0].Length;

        _matrix = new char[_rows, _cols];
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                _matrix[i, j] = matrixList[i][j];
            }
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> Find(IEnumerable<string> wordStream)
    {
        var wordFoundCounts = new Dictionary<string, int>();
        wordStream = wordStream.Select(item => item.ToLower()).ToList();
        foreach (var word in wordStream.Distinct())
        {
            int countOcurrencies = 0;

            for (int i = 0; i < _rows; i++)
            {
                countOcurrencies += SearchRow(word, i);
            }

            for (int j = 0; j < _cols; j++)
            {
                countOcurrencies += SearchColumn(word, j);
            }

            if (countOcurrencies > 0)
            {
                wordFoundCounts[word] = countOcurrencies;
            }
        }

        return wordFoundCounts
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .Take(10)
            .Select(kvp => kvp.Key)
            .ToList();
    }


    /// <summary>
    /// Counts the occurrences of a word in a specific row.
    /// </summary>
    /// <param name="word">The word to search for.</param>
    /// <param name="row">The row index.</param>
    /// <returns>The number of times the word appears in the row.</returns>
    private int SearchRow(string word, int row)
    {
        int count = 0;
        var rowString = new string(Enumerable.Range(0, _cols).Select(col => _matrix[row, col]).ToArray());

        int index = rowString.IndexOf(word, StringComparison.Ordinal);
        while (index != -1)
        {
            count++;
            index = rowString.IndexOf(word, index + 1, StringComparison.Ordinal);
        }

        return count;
    }


    /// <summary>
    /// Counts the occurrences of a word in a specific column.
    /// </summary>
    /// <param name="word">The word to search for.</param>
    /// <param name="col">The column index.</param>
    /// <returns>The number of times the word appears in the column.</returns>
    private int SearchColumn(string word, int col)
    {
        int count = 0;
        var colString = new string(Enumerable.Range(0, _rows).Select(row => _matrix[row, col]).ToArray());

        int index = colString.IndexOf(word, StringComparison.Ordinal);
        while (index != -1)
        {
            count++;
            index = colString.IndexOf(word, index + 1, StringComparison.Ordinal);
        }

        return count;
    }

}

