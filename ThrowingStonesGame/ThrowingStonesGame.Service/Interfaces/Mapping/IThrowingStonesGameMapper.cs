using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Application.Interfaces.Mapping
{
    public interface IThrowingStonesGameMapper
    {
        public List<PlayInfosModel> MapPlayInfos(List<PlayModel> plays);
        public RankingModel MapRankingModel(Ranking ranking);
    }
}
