using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Tests.Application
{
    public class PlayerTest
    {
        private Player _playerMock = new Player("Egio");
        public PlayerTest()
        {

        }

        [Theory]
        [InlineData(11, 2)]
        public void IncrementStoneJumps_WithSuccess(int stoneJumps, int bonus)
        {
            _playerMock.IncrementStoneJumps(stoneJumps, bonus);

            Assert.Equal(_playerMock.StoneJumpsCount, stoneJumps + bonus);


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
}
