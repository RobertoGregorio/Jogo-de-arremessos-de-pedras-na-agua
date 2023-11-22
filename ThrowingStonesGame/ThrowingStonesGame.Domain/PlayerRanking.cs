namespace ThrowingStonesGame.Domain;

public class PlayerRanking
{
    public string? Name { get; set; }
    public int WinsCountOutHome { get; set; }
    public bool IsChampion { get; set; }
    public int WinsCount { get; set; }
    public int TotalBonusCount { get; set; }
    public int InterestPointsCount { get; set; }
    public int TotalScores { get; set; }
    public int RankingPosition { get; set; }

    public PlayerRanking(string name)
    {
        Name = name;
    }

    public void ValidateInterestPoint(int stoneJumps)
    {
        if (stoneJumps < 3)
        {
            TotalScores--;
            InterestPointsCount++;
        }
    }

}