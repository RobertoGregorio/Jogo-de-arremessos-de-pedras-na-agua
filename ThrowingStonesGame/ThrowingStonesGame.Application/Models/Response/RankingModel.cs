using System.Text.Json.Serialization;

namespace ThrowingStonesGame.Application.Models.Response;
public class RankingModel
{
    [JsonPropertyName("qto_total_partidas")]
    public int MatchesTotalCount { get; set; }

    [JsonPropertyName("ranque_geral")]
    public List<PlayerRankingModel> PlayerRankingModels { get; set; } = new List<PlayerRankingModel>();
}

