using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Tests.Application
{
    public class PlayerTest
    {
        [Theory]
        [InlineData(11, 2)]
        [InlineData(20, 3)]
        public void IncrementStoneJumps_WithSuccess_SetCorrectStoneJumpsCount(int stoneJumps, int stoneJumpsBonus)
        {
            //Arrange

            Player playerMock = new("Egio");
            var expectedStoneJumpsCount = stoneJumps + stoneJumpsBonus;


            //Act
            playerMock.IncrementStoneJumps(stoneJumps, stoneJumpsBonus);


            //Assertion
            Assert.True(playerMock.StoneJumpsCount > 0);
            Assert.True(playerMock.BonusStoneJumpsCount > 0);
            Assert.Equal(playerMock.StoneJumpsCount, expectedStoneJumpsCount);
        }

        [Fact]
        public void IncrementStoneJumps_WhenBonusIsZero_SetCorrectStoneJumpsCount()
        {
            //Arrange

            Player playerMock = new("Egio") { StoneJumpsCount  = 11 , };
            var stoneJumpsBonus = 0;
            var expectedStoneJumpsCount = playerMock.StoneJumpsCount + stoneJumpsBonus;

            //Act
            playerMock.IncrementStoneJumps(stoneJumpsBonus, stoneJumpsBonus);
          
            //Assertion
            Assert.Equal(playerMock.StoneJumpsCount, expectedStoneJumpsCount);
        }

    }
}

