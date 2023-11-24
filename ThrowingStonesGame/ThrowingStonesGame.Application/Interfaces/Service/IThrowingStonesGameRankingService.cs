using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Application.Interfaces.Service;

public interface IThrowingStonesGameRankingService
{
    public Ranking GenerateRanking(List<Match> matches);
}
