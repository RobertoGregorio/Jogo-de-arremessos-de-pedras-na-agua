namespace ThrowingStonesGame.Domain;

public class Player
{
    public string? Name { get; set; }
    public int TotalStoneJumps { get; set; }
    public bool IsWinner { get; set; }
    public bool PlayingOutHome { get; set; }
    public int TotalBonusStoneJumpsCount { get; set; }
    public List<int> StoneJumpsHistoricCountPerPlay { get; set; } = new List<int>();

    public Player(string name, bool playingOutHome = false)
    {
        Name = name;
        PlayingOutHome = playingOutHome;
    }

    public void IncrementStoneJumps(int stoneJumps, int bonus = 0)
    {
        IncrementBonusStoneJumpsCount(bonus);

        stoneJumps = stoneJumps + bonus;
        TotalStoneJumps += stoneJumps;
        StoneJumpsHistoricCountPerPlay.Add(stoneJumps);
    }

    public void IncrementStoneJumps(int bonus)
    {
        IncrementBonusStoneJumpsCount(bonus);

        TotalStoneJumps += bonus;
    }

    private void IncrementBonusStoneJumpsCount(int bonus = 0)
    {
        if (bonus > 0)
            TotalBonusStoneJumpsCount++;
    }
}