using Microsoft.AspNetCore.Mvc;
using QuChallenge.Application.Dtos;
using QuChallenge.Application.Interfaces;

namespace QuChallenge.Api.Controllers;

/// <summary>
/// Controller for handling word search requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WordFinderController : ControllerBase
{
    private readonly IWordFinderService _wordFinderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WordFinderController"/> class.
    /// </summary>
    /// <param name="wordFinderService">The service for processing word search operations.</param>
    public WordFinderController(IWordFinderService wordFinderService)
    {
        _wordFinderService = wordFinderService;
    }

    /// <summary>
    /// Finds words in a given matrix based on the word stream provided.
    /// </summary>
    /// <param name="request">The matrix and word stream to search.</param>
    /// <returns>A list of found words.</returns>
    [HttpPost("find")]
    public IActionResult FindWords([FromBody] WordFinderRequestDto request)
    {
        if (request.Matrix == null || !request.Matrix.Any() || request.WordStream == null || !request.WordStream.Any()) 
        {
            return BadRequest("Matrix and word stream cannot be null.");
        }

        if (request.Matrix.Count() > 64 || request.Matrix.Any(row => row.Length > 64))
        {
            return BadRequest("Matrix size must not exceed 64x64.");
        }

        if (request.Matrix.Any(row => row.Length != request.Matrix.First().Length))
        {
            return BadRequest("Matrix rows must have the same length.");
        }

        if (request.Matrix.Any(row => row.Any(ch => !char.IsLetter(ch))))
        {
            return BadRequest("Matrix must contain only alphabetic characters.");
        }

        if (request.WordStream == null || !request.WordStream.Any())
        {
            return BadRequest("Word stream must not be null or empty.");
        }

        var result = _wordFinderService.FindWords(request);

        if (!result.Any())
        {
            return Ok("No words were found in the matrix.");
        }

        return Ok(result);
    }

}
