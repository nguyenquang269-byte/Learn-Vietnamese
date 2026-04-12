using Noihay.Web.Services;

namespace Noihay.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("lesson/{id}")]
    public async Task<IActionResult> GetLesson(int id)
    {
        var result = await _gameService.GetLessonLevelAsync(id);
        return Ok(result);
    }

    [HttpGet("monkey")]
    public async Task<IActionResult> GetMonkeyLevel()
    {
        var result = await _gameService.GetSpellingMonkeyLevelAsync();
        return Ok(result);
    }
}
