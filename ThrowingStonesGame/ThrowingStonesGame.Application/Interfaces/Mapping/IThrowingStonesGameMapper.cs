﻿using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Application.Models.Response;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Application.Interfaces.Mapping;

public interface IThrowingStonesGameMapper
{
    public List<PlayInfosModel> MapPlayInfosList(List<PlayModel> plays);
    public RankingModel MapRankingModel(Ranking ranking);
}
