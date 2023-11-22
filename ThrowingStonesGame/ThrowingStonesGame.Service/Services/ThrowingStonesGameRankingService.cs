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
                PlayerRanking fisrtPlayerRanking = rank.AddIfNotExistPlayerRanking(match.FirstPlayer.Name);
                fisrtPlayerRanking.TotalBonusCount += match.FirstPlayer.BonusStoneJumpsCount;

                PlayerRanking secondPlayerRanking = rank.AddIfNotExistPlayerRanking(match.SecondPlayer.Name);
                secondPlayerRanking.TotalBonusCount += match.SecondPlayer.BonusStoneJumpsCount;

                if (match.IsTie)
                    secondPlayerRanking.TotalScores += ApplicationConstants.TiePotuation;
                else
                {
                    if (match.FirstPlayer.IsWinner)
                    {
                        fisrtPlayerRanking.TotalScores += ApplicationConstants.WinPotuation;
                        fisrtPlayerRanking.WinsCount++;
                    }
                    else
                    {
                        secondPlayerRanking.TotalScores += ApplicationConstants.WinPotuation;
                        secondPlayerRanking.WinsCount++;
                        secondPlayerRanking.WinsCountOutHome++;
                    }
                }

                fisrtPlayerRanking.ValidateInterestPoint(match.FirstPlayer.StoneJumpsCount);
                secondPlayerRanking.ValidateInterestPoint(match.SecondPlayer.StoneJumpsCount);
            }

            rank.GeneralClassification = rank.GeneralClassification.SortedRanking();

            return rank;
        }
    }
}
