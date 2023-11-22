using Microsoft.AspNetCore.Mvc;
using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.Application.Interfaces.Mapping;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Models;

namespace ThrowingStonesGame.API.Controllers;

[ApiController]
[Route("campeonato")]
public class ThrowingStonesGameController : ControllerBase
{
    private readonly ILogger<ThrowingStonesGameController> _logger;
    private IThrowingStonesGameMapper _gameMapper;
    private readonly IThrowingStonesGameService _gameService;
    private readonly IThrowingStonesGameRankingService _gameRankingValidator;

    public ThrowingStonesGameController(ILogger<ThrowingStonesGameController> logger, IThrowingStonesGameService gameService, IThrowingStonesGameMapper gameMapper, IThrowingStonesGameRankingService gameRanking)
    {
        _logger = logger;
        _gameService = gameService;
        _gameMapper = gameMapper;
        _gameRankingValidator = gameRanking;
    }

    [TypeFilter(typeof(RequestLogFilterHandler))]
    [HttpPost("classificacao_geral")]
    public ActionResult GenerateGameClassification(GameModel gameModelJson)
    {
        try
        {
            var plays = _gameMapper.MapPlayInfos(gameModelJson.Plays);

            var results = _gameService.GetMatches(plays);

            var ranking = _gameRankingValidator.GenerateRanking(results);

            var rankingModel = _gameMapper.MapRankingModel(ranking);

            return Ok(rankingModel);
        }
        catch (Exception error)
        {
            _logger.LogError("{1} error:{0}", DateTime.UtcNow, error.Message);
            return StatusCode(500, new { msgErro = error.Message });
        }
    }
}
