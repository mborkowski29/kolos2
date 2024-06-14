using kolos2.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolos2.Controllers;

[ApiController]
[Route("api/characters")]
public class Controller : ControllerBase
{
    private readonly IdService _service;

    public Controller(IdService service)
    {
        _service = service;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacter(int characterId)
    {
        var character = await _service.getCharacter(characterId);
        if (character == null)
        {
            return NotFound();
        }
        return Ok(character);
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> DodajDoPlecaka(int characterId, List<int> items)
    {
        try
        {
            var result = await _service.AddItemsToBackpack(characterId, items);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest($"blad podczas: {e.Message}");
        }
    }
}