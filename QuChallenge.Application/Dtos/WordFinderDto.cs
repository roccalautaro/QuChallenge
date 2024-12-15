using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuChallenge.Application.Dtos;

/// <summary>
/// Represents the input for the WordFinder API.
/// </summary>
public class WordFinderRequestDto
{
    /// <summary>
    /// The character matrix to search in.
    /// </summary>
    public IEnumerable<string>? Matrix { get; set; }

    /// <summary>
    /// The word stream to search for.
    /// </summary>
    public IEnumerable<string>? WordStream { get; set; }
}
