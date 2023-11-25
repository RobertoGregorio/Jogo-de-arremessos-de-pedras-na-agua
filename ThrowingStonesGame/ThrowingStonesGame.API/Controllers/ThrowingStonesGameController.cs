using Microsoft.AspNetCore.Mvc;
using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.Application.Interfaces.Mapping;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Models.Request;
using ThrowingStonesGame.Application.Models.Response;

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

    [HttpGet("Index")]
    public ContentResult Index()
    {
        using StreamReader reader = new StreamReader(@"Index\index.html");
        var htmlContext = reader.ReadToEnd();
        return new ContentResult
        {
            Content = htmlContext,
            ContentType = "text/html"
        };
    }

    [TypeFilter(typeof(RequestLogFilterHandler))]
    [HttpPost("classificacao_geral")]
    public ActionResult GenerateGameClassification([FromBody] GameModel gameModelJson)
    {
        try
        {
            if (gameModelJson.Plays != null && !gameModelJson.Plays.Any())
                return BadRequest(new ModelValidation("A lista de jogadas não pode ser nula ou vazia"));

            var playsInfoList = _gameMapper.MapPlayInfosList(gameModelJson.Plays);
            var matches = _gameService.GetMatches(playsInfoList);
            var ranking = _gameRankingValidator.GenerateRanking(matches);
            var rankingModel = _gameMapper.MapRankingModel(ranking);
            rankingModel.MatchesTotalCount = matches.Count;

            return Ok(rankingModel);
        }
        catch (Exception error)
        {
            _logger.LogError($"{DateTime.UtcNow} - error: {error.Message}");
            return StatusCode(500, new { msgErro = error.Message });
        }
    }
}
