using ThrowingStonesGame.Application.Constants;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Application.Services;

namespace ThrowingStonesGame.Tests.Application
{
    public class ThrowingStonesGameServiceTest
    {
        private IThrowingStonesGameService _throwingStonesGameServiceMock;
        private List<PlayInfosModel> _playInfosModelListMock;

        public ThrowingStonesGameServiceTest()
        {
            _playInfosModelListMock = new List<PlayInfosModel>()
            {
                { new PlayInfosModel( "Egio x Jaco", "4 x 2" )},
                { new PlayInfosModel( "Egio x Jaco",  "2 x 5" )},
                { new PlayInfosModel( "Egio x Jaco",  "0 x 3" )},
                { new PlayInfosModel( "Jaco x Egio",  "2 x 2" )},
                { new PlayInfosModel( "Jaco x Egio",  "2 x 6" )},
                { new PlayInfosModel( "Jaco x Egio",  "2 x 2" )},
            };



            _throwingStonesGameServiceMock = new ThrowingStonesGameService();
        }


        [Fact]
        public void GetMatches_WithSucces_ReturnedMatches()
        {
            //Arrange
            var matches = _throwingStonesGameServiceMock.GetMatches(_playInfosModelListMock);

            //Act
            int totalMatches = 2;

            //Assertion
            Assert.True(matches != null);
            Assert.Equal(totalMatches, matches.Count);

            foreach (var match in matches)
            {
                Assert.True(match.FirstPlayer != null);
                Assert.True(match.SecondPlayer != null);
            }
        }

        [Fact]
        public void GetMatches_WhenPlaysCountIsZero_ReturnedMatches()
        {
            //Arrange
            var matches = _throwingStonesGameServiceMock.GetMatches(new List<PlayInfosModel>());

            //Act
            int expectedTotalMatches = 0;

            //Assertion
            Assert.True(matches != null);
            Assert.Equal(expectedTotalMatches, matches.Count);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(20)]
        [InlineData(30)]
        public void GetBonusStoneJumpsInThePlay_WhenStoneJumpMoreThan10InThePlay_ReturnedCorrectBonusValue(int stoneJumpsCount)
        {
            //Arrange
            var expectedStoneJumpsBonusValue = GameRulesConstants.StoneJumpBonusValue;

            //Act
            var stoneJumpsBonusValue = _throwingStonesGameServiceMock.GetBonusStoneJumpsInThePlay(stoneJumpsCount);

            //Assertion
            Assert.Equal(stoneJumpsBonusValue, expectedStoneJumpsBonusValue);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void GetBonusStoneJumpsInThePlay_WhenStoneJumpLessThan10InThePlay_ReturnedZeroBonusValue(int stoneJumpsCount)
        {
            //Arrange
            var expectedStoneJumpBonus = 0;

            //Act
            var stoneJumpsBonusValue = _throwingStonesGameServiceMock.GetBonusStoneJumpsInThePlay(stoneJumpsCount);

            //Assertion
            Assert.Equal(stoneJumpsBonusValue, expectedStoneJumpBonus);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void GetBonusStoneJumpsInTheMatch_WhenStoneJumpsInAllPlaysIsEquals_ReturnedCorrectBonusValue(int stoneJumpsCount)
        {
            //Arrange
            List<int> stoneJumpsValuesInThePlays = new List<int>() { stoneJumpsCount, stoneJumpsCount, stoneJumpsCount };
            var expectedStoneJumpsBonus = Math.Round(stoneJumpsValuesInThePlays.Sum() * GameRulesConstants.StoneJumpAdditionalPercentage / 100D);

            //Act
            var stoneJumpsBonusValue = _throwingStonesGameServiceMock.GetBonusStoneJumpsInTheMatch(stoneJumpsValuesInThePlays);
            
            //Assertion
            Assert.Equal(stoneJumpsBonusValue, expectedStoneJumpsBonus);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void GetBonusStoneJumpsInTheMatch_WhenStoneJumpsInAllPlaysIsDiferent_ReturnZeroBonus(int stoneJumpsCount)
        {
            //Arrange
            List<int> stoneJumpsValuesInThePlays = new List<int>() { stoneJumpsCount, stoneJumpsCount + 1, stoneJumpsCount + 2 };
            var expectedStoneJumpsBonus = 0;

            //Act
            var stoneJumpsBonusValue = _throwingStonesGameServiceMock.GetBonusStoneJumpsInTheMatch(stoneJumpsValuesInThePlays);

            //Assertion
            Assert.Equal(stoneJumpsBonusValue, expectedStoneJumpsBonus);
           
        }
    }
}