using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Application.Extensions;

public static class PlayRankingExtensions
{
    public static List<PlayerRanking> SortedRanking(this List<PlayerRanking> generalClassification)
    {
        generalClassification = generalClassification.OrderByClassificationDesc();

        var position = 1;

        foreach (var playerRanking in generalClassification)
            playerRanking.RankingPosition = position++;

        return generalClassification;

    }

    public static List<PlayerRanking>? OrderByClassificationDesc(this List<PlayerRanking> generalClassification)
    {
        var stotalScoreMax = generalClassification.MaxBy(x => x.TotalScores).TotalScores;
        var tieRanking = generalClassification.Select(x => x).Where(x => x.TotalScores == stotalScoreMax).ToList();

        if (tieRanking != null && tieRanking.Count > 1)
        {
            var winsCountMaxValue = generalClassification.MaxBy(x => x.WinsCount).WinsCount;

            tieRanking = generalClassification.Select(x => x).Where(x => x.WinsCount == winsCountMaxValue).ToList();

            if (tieRanking.Count > 1)
            {
                var winsOutHomeMaxValue = generalClassification.MaxBy(x => x.WinsCountOutHome).WinsCountOutHome;
                tieRanking = generalClassification.Select(x => x).Where(x => x.WinsCountOutHome == winsOutHomeMaxValue).ToList();
            }
            else
                return generalClassification.OrderByDescending(x => x.WinsCount).ToList();

            if (tieRanking.Count > 1)
            {
                var totalBonusCountMaxValue = generalClassification.MaxBy(x => x.TotalBonusCount).TotalBonusCount;
                tieRanking = generalClassification.Select(x => x).Where(x => x.TotalBonusCount == totalBonusCountMaxValue).ToList();
            }
            else
                return generalClassification = generalClassification.OrderByDescending(x => x.WinsCountOutHome).ToList();

            if (tieRanking.Count > 1)
            {
                var interestPointsCountMinalue = generalClassification.MinBy(x => x.InterestPointsCount).InterestPointsCount;
                tieRanking = generalClassification.Select(x => x).Where(x => x.InterestPointsCount == interestPointsCountMinalue).ToList();
            }
            else
                return generalClassification.OrderByDescending(x => x.TotalBonusCount).ToList();

            return generalClassification.OrderBy(x => x.InterestPointsCount).ToList();
        }

        return generalClassification.OrderByDescending(x => x.TotalScores).ToList();
    }
}