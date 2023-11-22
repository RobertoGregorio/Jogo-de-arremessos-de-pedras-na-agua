using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Application.Extensions
{
    public static class PlayRankingExtensions
    {
        public static List<PlayerRanking> SortedRanking(this List<PlayerRanking> generalClassification)
        {
            generalClassification = generalClassification.OrderByClassification();

            var position = 1;

            foreach (var playerRanking in generalClassification)
                playerRanking.RankingPosition = position++;

            return generalClassification;

        }

        public static List<PlayerRanking>? OrderByClassification(this List<PlayerRanking> generalClassification)
        {
            var tieRanking = generalClassification.Select(x => x).Where(x => x.TotalScores == generalClassification.MaxBy(x => x.TotalScores).TotalScores).ToList();

            if (tieRanking != null && tieRanking.Count > 1)
            {
                tieRanking = generalClassification.Select(x => x).Where(x => x.WinsCount == generalClassification.MaxBy(x => x.WinsCount).WinsCount).ToList();

                if (tieRanking.Count > 1)
                    tieRanking = generalClassification.Select(x => x).Where(x => x.WinsCountOutHome == generalClassification.MaxBy(x => x.WinsCountOutHome).WinsCountOutHome).ToList();
                else
                    return generalClassification.OrderByDescending(x => x.WinsCount).ToList();

                if (tieRanking.Count > 1)
                    tieRanking = generalClassification.Select(x => x).Where(x => x.TotalBonusCount == generalClassification.MaxBy(x => x.TotalBonusCount).TotalBonusCount).ToList();
                else
                    return generalClassification = generalClassification.OrderByDescending(x => x.WinsCountOutHome).ToList();

                if (tieRanking.Count > 1)
                    tieRanking = generalClassification.Select(x => x).Where(x => x.InterestPointsCount == generalClassification.MinBy(x => x.InterestPointsCount).InterestPointsCount).ToList();
                else
                    return generalClassification.OrderByDescending(x => x.TotalBonusCount).ToList();

                return generalClassification.OrderBy(x => x.InterestPointsCount).ToList();

            }

            return generalClassification.OrderByDescending(x => x.TotalScores).ToList();
        }
    }
}