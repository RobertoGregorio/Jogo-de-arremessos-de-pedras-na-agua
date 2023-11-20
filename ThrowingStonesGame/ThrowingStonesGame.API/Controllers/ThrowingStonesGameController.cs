using Microsoft.AspNetCore.Mvc;
using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Service.Interfaces;

namespace ThrowingStonesGame.API.Controllers;

[ApiController]
[Route("campeonato")]
public class ThrowingStonesGameController : ControllerBase
{
    private readonly ILogger<ThrowingStonesGameController> _logger;
    private readonly IThrowingStonesGameService _throwingStonesGameService;

    public ThrowingStonesGameController(ILogger<ThrowingStonesGameController> logger, IThrowingStonesGameService throwingStonesGameService)
    {
        _logger = logger;
        _throwingStonesGameService = throwingStonesGameService;
    }

    [TypeFilter(typeof(RequestLogFilterHandler))]
    [HttpPost("classificacao_geral")]
    public ActionResult GenerateChampionshipClassification(GameModel game)
    {

        var a = _throwingStonesGameService.GenerateClassification(game.Plays);


        return Ok(new
        {
            vencedor = game.Plays[0],
            ranking = a
        });
    }
}
