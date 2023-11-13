
using Microsoft.AspNetCore.Mvc;
using ThrowingStonesGame.API.Models;

namespace ThrowingStonesGame.API.Controllers;

[ApiController]
[Route("campeonato")]
public class ThrowingStonesGameController : ControllerBase
{
    private readonly ILogger<ThrowingStonesGameController> _logger;

    // private IThrowingStonesGameService throwingStonesGameService;

    public ThrowingStonesGameController(ILogger<ThrowingStonesGameController> logger)
    {
        _logger = logger;
    }

    [HttpPost("classificacao_geral")]
    public ActionResult GenerateChampionshipClassification(GameMatchModel gameMatchInputModel)
    {
        _logger.LogInformation($"Request received{DateTime.UtcNow} input model: {gameMatchInputModel} ");

        return Ok(new
        {
            vencedor = gameMatchInputModel.Plays != null ? gameMatchInputModel.Plays[0].Players : null,
            ranking = "null"

        });
    }
}
