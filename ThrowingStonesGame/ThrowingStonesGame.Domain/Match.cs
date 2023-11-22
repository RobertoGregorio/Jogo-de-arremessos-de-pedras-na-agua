namespace ThrowingStonesGame.Domain
{
    public class Match
    {
        public int[] MatchScoreboard { get; set; }
        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }
        public bool IsTie { get; private set; }

        public Match(Player firstPlayer, Player secondPlayer)
        {
            if (firstPlayer == null || secondPlayer == null)
                throw new Exception("um dos jogadores da partida não podem ser nulo");

            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
        }

        public void ComputeWinner()
        {
            if (!IsTie)
            {
                if (FirstPlayer.StoneJumpsCount > SecondPlayer.StoneJumpsCount)
                    FirstPlayer.IsWinner = true;
                else
                    SecondPlayer.IsWinner = true;
            }
        }

        public void SetMatchScoreboard()
        {
            MatchScoreboard = new int[] { FirstPlayer.StoneJumpsCount, SecondPlayer.StoneJumpsCount };

            if (FirstPlayer.StoneJumpsCount == SecondPlayer.StoneJumpsCount)
                IsTie = true;
        }
    }
}