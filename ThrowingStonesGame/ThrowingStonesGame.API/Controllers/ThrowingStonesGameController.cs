
using Microsoft.AspNetCore.Mvc;
using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.API.Models;
using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;

namespace ThrowingStonesGame.API.Controllers;

[ApiController]
[Route("campeonato")]
public class ThrowingStonesGameController : ControllerBase
{
    private readonly ILogger<ThrowingStonesGameController> _logger;
    private readonly IServiceBusProducer serviceBusProducer;
    // private IThrowingStonesGameService throwingStonesGameService;

    public ThrowingStonesGameController(ILogger<ThrowingStonesGameController> logger)
    {
        _logger = logger;
    }

    [TypeFilter(typeof(RequestLogFilterHandler))]
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
