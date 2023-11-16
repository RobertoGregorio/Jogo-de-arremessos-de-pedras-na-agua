using ThrowingStonesGame.Service.Dtos;

namespace ThrowingStonesGame.Service.Interfaces
{
    public interface IThrowingStonesGameService
    {
        List<PlayerClassification> GenerateClassification(List<PlayDto> plays);

        public List<MatchDto> OrganizeMatchesResults(List<PlayDto> plays);

        public MatchDto GetMatchResult(List<PlayDto> plays);

        public int CalculateTheStoneJumpBonusInThePlay(int stoneJumpsCount);

        public int CalculateTotalStoneJumpBonusInTheMatch(List<int> stoneJumpingCountInThePlays, int stoneJumpingTotalCount);
    }
}
