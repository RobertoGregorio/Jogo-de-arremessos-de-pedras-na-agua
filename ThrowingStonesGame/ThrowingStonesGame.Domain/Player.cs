namespace ThrowingStonesGame.Domain
{
    public class Player
    {
        public Player() => StoneJumpingCountInThePlays = new List<int>();

        public string? Name { get; set; }
        public int StoneJumpingTotalCountInTheMatch { get; set; }
        public bool PlayingAwayFromHome { get; set; }
        public bool Winner { get; set; }
        public List<int> StoneJumpingCountInThePlays { get; set; }
        public int MatchScore { get; set; }
    }
}
