using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Service.Interfaces
{
    public interface IThrowingStonesGameService
    {
        List<GameResultInfos> GenerateClassification(List<PlayModel> plays);

        public List<Match> GetMatchesResults(List<Play> plays);

        public Match GetMatchResult(List<Play> plays);

        public int CalculateTheStoneJumpBonusInThePlay(int stoneJumpsCount);

        public int CalculateTotalStoneJumpBonusInTheMatch(List<int> stoneJumpingCountInThePlays, int stoneJumpingTotalCount);
    }
}
