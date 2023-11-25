using ThrowingStonesGame.Application.Constants;
using ThrowingStonesGame.Application.Extensions;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Application.Services
{
    public class ThrowingStonesGameRankingService : IThrowingStonesGameRankingService
    {
        public Ranking GenerateRanking(List<Match> matches)
        {
            var rank = new Ranking();

            foreach (Match match in matches)
            {
                PlayerRanking fisrtPlayerRanking = rank.AddDoesNotExistPlayerRanking(match.FirstPlayer.Name);
                fisrtPlayerRanking.TotalStoneJumpsBonusCount += match.FirstPlayer.TotalBonusStoneJumpsCount;

                PlayerRanking secondPlayerRanking = rank.AddDoesNotExistPlayerRanking(match.SecondPlayer.Name);
                secondPlayerRanking.TotalStoneJumpsBonusCount += match.SecondPlayer.TotalBonusStoneJumpsCount;

                if (match.IsTie)
                    secondPlayerRanking.TotalScores += GameRulesConstants.TiePotuation;
                else
                {
                    if (match.FirstPlayer.IsWinner)
                    {
                        fisrtPlayerRanking.TotalScores += GameRulesConstants.WinPotuation;
                        fisrtPlayerRanking.TotalWins++;
                    }
                    else
                    {
                        secondPlayerRanking.TotalScores += GameRulesConstants.WinPotuation;
                        secondPlayerRanking.TotalWins++;
                        secondPlayerRanking.TotalWinsOutHome++;
                    }
                }

                if (match.FirstPlayer.TotalStoneJumps < GameRulesConstants.MinTotalStoneJumpsinTheMatchAllowed)
                    fisrtPlayerRanking.AddPunishment();

                if (match.SecondPlayer.TotalStoneJumps < GameRulesConstants.MinTotalStoneJumpsinTheMatchAllowed)
                    secondPlayerRanking.AddPunishment();

            }

            rank.GeneralClassification = rank.GeneralClassification.SortedRanking();

            return rank;
        }
    }
}
