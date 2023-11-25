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
        if (!generalClassification.GetElementWithMaxScoresValue().HasMoreOnePlayer())
            return generalClassification.OrderByDescending(x => x.TotalScores).ToList();

        if (!generalClassification.GetElementWithMaxWinsCount().HasMoreOnePlayer())
            return generalClassification.OrderByDescending(x => x.TotalWins).ToList();

        if (!generalClassification.GetElementWithMaxWinsOutHomeCount().HasMoreOnePlayer())
            return generalClassification.OrderByDescending(x => x.TotalWinsOutHome).ToList();

        if (!generalClassification.GetElementWithMaxTotalBonusCount().HasMoreOnePlayer())
            return generalClassification.OrderByDescending(x => x.TotalStoneJumpsBonusCount).ToList();

        return generalClassification.OrderBy(x => x.TotalPunishmentPoints).ToList();
    }

    public static bool HasMoreOnePlayer(this List<PlayerRanking> tieRanking) => tieRanking.Count > 1;
    public static List<PlayerRanking>? GetElementWithMaxScoresValue(this List<PlayerRanking> generalClassification) => generalClassification.Select(x => x).Where(x => x.TotalScores == generalClassification.MaxBy(x => x.TotalScores).TotalScores).ToList();
    public static List<PlayerRanking>? GetElementWithMaxWinsCount(this List<PlayerRanking> generalClassification) => generalClassification.Select(x => x).Where(x => x.TotalWins == generalClassification.MaxBy(x => x.TotalWins).TotalWins).ToList();
    public static List<PlayerRanking>? GetElementWithMaxWinsOutHomeCount(this List<PlayerRanking> generalClassification) => generalClassification.Select(x => x).Where(x => x.TotalWinsOutHome == generalClassification.MaxBy(x => x.TotalWinsOutHome).TotalWinsOutHome).ToList();
    public static List<PlayerRanking>? GetElementWithMaxTotalBonusCount(this List<PlayerRanking> generalClassification) => generalClassification.Select(x => x).Where(x => x.TotalStoneJumpsBonusCount == generalClassification.MaxBy(x => x.TotalStoneJumpsBonusCount).TotalStoneJumpsBonusCount).ToList();
    public static List<PlayerRanking>? GetElementWithMinTotalInterestPointsCount(this List<PlayerRanking> generalClassification) => generalClassification.Select(x => x).Where(x => x.TotalPunishmentPoints == generalClassification.MinBy(x => x.TotalPunishmentPoints).TotalPunishmentPoints).ToList();
}