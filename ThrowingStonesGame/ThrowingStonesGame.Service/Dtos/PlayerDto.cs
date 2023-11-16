namespace ThrowingStonesGame.Domain
{
    public class PlayerDto
    {
        public PlayerDto()
        {
            StoneJumpingCountInThePlays = new List<int>();
        }

        public string Name { get; set; }
        public int StoneJumpingTotalCount { get; set; }
        public int Score { get; set; }
        public List<int> StoneJumpingCountInThePlays { get; set; }

    }
}
