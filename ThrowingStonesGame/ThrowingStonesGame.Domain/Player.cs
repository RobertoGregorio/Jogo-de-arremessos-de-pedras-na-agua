namespace ThrowingStonesGame.Domain
{
    public class Player
    {
        public string? Name { get; set; }
        public int StoneJumpsCount { get; set; }
        public bool IsWinner { get; set; }
        public bool PlayingOutHome { get; set; }
        public int BonusStoneJumpsCount { get; set; }
        public List<int> StoneJumpsCountHistoric { get; set; } = new List<int>();

        public Player(string name, bool playingOutHome = false)
        {
            Name = name;
            PlayingOutHome = playingOutHome;
        }

        public void IncrementStoneJumps(int stoneJumps, int bonus = 0)
        {
            IncrementBonusStoneJumpsCount(bonus);

            stoneJumps = stoneJumps + bonus;
            StoneJumpsCount += stoneJumps;
            StoneJumpsCountHistoric.Add(stoneJumps);
        }

        public void IncrementStoneJumps(int bonus)
        {
            IncrementBonusStoneJumpsCount(bonus);

            StoneJumpsCount += bonus;
        }

        private void IncrementBonusStoneJumpsCount(int bonus = 0)
        {
            if (bonus > 0)
                BonusStoneJumpsCount++;
        }
    }
}