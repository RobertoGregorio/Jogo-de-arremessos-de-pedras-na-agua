using API.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("campeonato")]
public class ChampioshipController : ControllerBase
{
    private readonly ILogger<ChampioshipController> _logger;

    public ChampioshipController(ILogger<ChampioshipController> logger)
    {
        _logger = logger;
    }

    [HttpPost("classificacao_geral")]
    public ActionResult GerarClassificacaoGeral(GameMatchVO gameMatchVO)
    {



        _logger.LogInformation($"Request reived{DateTime.UtcNow} vo: {gameMatchVO} ");
        return Ok(new { message = "Jogadas recebidas" });
    }
}
