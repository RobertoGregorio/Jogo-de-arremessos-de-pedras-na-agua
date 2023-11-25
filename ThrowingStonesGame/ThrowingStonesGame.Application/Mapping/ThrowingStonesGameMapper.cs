using ThrowingStonesGame.Application.Interfaces.Mapping;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Application.Models.Response;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.API.Mapping;

public class ThrowingStonesGameMapper : IThrowingStonesGameMapper
{
    public List<PlayInfosModel> MapPlayInfosList(List<PlayModel> plays)
    {
        var mappingList = new List<PlayInfosModel>();

        foreach (var model in plays)
        {
            mappingList.Add(new PlayInfosModel(model.Players, model.PlayResult));
        }

        return mappingList;
    }

    public RankingModel MapRankingModel(Ranking ranking)
    {
        var rankingModel = new RankingModel();

        foreach (var playerRankig in ranking.GeneralClassification)
        {
            rankingModel.PlayerRankingModelList.Add(new PlayerRankingModel(playerRankig.Name,
                playerRankig.TotalScores,
                playerRankig.RankingPosition,
                playerRankig.TotalWins,
                playerRankig.TotalStoneJumpsBonusCount,
                playerRankig.TotalPunishmentPoints));
        }

        return rankingModel;
    }
}