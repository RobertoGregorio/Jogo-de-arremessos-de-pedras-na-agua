﻿using System.Text.Json.Serialization;

namespace ThrowingStonesGame.Application.Models.Response;
public class RankingModel
{
    [JsonPropertyName("ranque_geral")]
    public List<PlayerRankingModel> PlayerRankingModels { get; set; } = new List<PlayerRankingModel>();
}
