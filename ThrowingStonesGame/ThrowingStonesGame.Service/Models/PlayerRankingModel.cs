using System.Text.Json.Serialization;

namespace ThrowingStonesGame.Application.Models
{
    public class PlayerRankingModel
    {
        [JsonPropertyName("nome")]
        public string? Name { get; set; }

        [JsonPropertyName("qto_total_pontos")]
        public int TotalScores { get; set; }

        [JsonPropertyName("posicao_ranking")]
        public int RankingPosition { get; set; }

        [JsonPropertyName("qto_vitorias")]
        public int WinsCount { get; set; }

        [JsonPropertyName("qto_bonus")]
        public int StoneJumpsBonusTotalCount { get; set; }

        [JsonPropertyName("qto_pontos_punicao")]
        public int InterestPointsTotalCount { get; set; }

        public PlayerRankingModel(string name, int totalScores, int rankingPosition, int winsCount, int stoneJumpsBonusCount, int interestPointsTotalCount)
        {
            Name = name;
            TotalScores = totalScores;
            RankingPosition = rankingPosition;
            WinsCount = winsCount;
            StoneJumpsBonusTotalCount = stoneJumpsBonusCount;
            StoneJumpsBonusTotalCount = stoneJumpsBonusCount;
            InterestPointsTotalCount = interestPointsTotalCount;
        }
    }
}
