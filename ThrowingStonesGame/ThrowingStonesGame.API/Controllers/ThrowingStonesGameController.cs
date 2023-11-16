
using Microsoft.AspNetCore.Mvc;
using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.API.Mapping.Interfaces;
using ThrowingStonesGame.API.Models.Jsons;
using ThrowingStonesGame.Service;
using ThrowingStonesGame.Service.Interfaces;

namespace ThrowingStonesGame.API.Controllers;

[ApiController]
[Route("campeonato")]
public class ThrowingStonesGameController : ControllerBase
{
    private readonly ILogger<ThrowingStonesGameController> _logger;
    private readonly IMapper _mapper;
    private readonly IThrowingStonesGameService _throwingStonesGameService;

    public ThrowingStonesGameController(ILogger<ThrowingStonesGameController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    [TypeFilter(typeof(RequestLogFilterHandler))]
    [HttpPost("classificacao_geral")]
    public ActionResult GenerateChampionshipClassification(GameModel game)
    {
        var matches = _mapper.MapperForPlayMatchDtoList(game.Plays);

        //_throwingStonesGameService.CalculeWinner(matches);

        //  _logger.LogInformation($"Request received{DateTime.UtcNow} input model: {plays} ");

        return Ok(new
        {
            vencedor = game.Plays[0],
            ranking = "null"
        });
    }
}
