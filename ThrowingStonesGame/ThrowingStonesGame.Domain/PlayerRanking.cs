namespace ThrowingStonesGame.Domain;

public class PlayerRanking
{
    public string? Name { get; set; }
    public int TotalWinsOutHome { get; set; }
    public int TotalWins { get; set; }
    public int TotalStoneJumpsBonusCount { get; set; }
    public int TotalPunishmentPoints { get; set; }
    public int TotalScores { get; set; }
    public int RankingPosition { get; set; }

    public PlayerRanking(string name) => Name = name;

    public void AddPunishment()
    {
        TotalScores--;
        TotalPunishmentPoints++;
    }
}