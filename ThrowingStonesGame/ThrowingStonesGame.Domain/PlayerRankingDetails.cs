namespace ThrowingStonesGame.Domain;

public class PlayerRankingDetails
{
    public string PlayerName { get; set; }
    public int FinalTotalScores { get; set; }
    public int WinnersCount { get; set; }
    public int OutHomeWinnersCount { get; set; }
    public int StoneJumpsBonusCount { get; set; }
    public int InterestPoinstCount { get; set; }
}