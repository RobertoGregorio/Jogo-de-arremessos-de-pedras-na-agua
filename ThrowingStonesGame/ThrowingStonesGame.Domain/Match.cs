namespace ThrowingStonesGame.Domain
{
    public class Match
    {
        public Match() => Players = new List<Player>();

        public List<Player> Players { get; set; }
    
    }
}