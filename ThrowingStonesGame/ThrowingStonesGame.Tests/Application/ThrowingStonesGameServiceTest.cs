using ThrowingStonesGame.API.Mapping;
using ThrowingStonesGame.Application.Interfaces.Mapping;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Application.Services;

namespace ThrowingStonesGame.Tests.Application
{
    public class ThrowingStonesGameServiceTest
    {
        private IThrowingStonesGameService _throwingStonesGameServiceMock;
        private List<PlayModel> playModelsMock = new List<PlayModel>();
        private IThrowingStonesGameMapper _mapper;

        public void Setup()
        {
            playModelsMock.Add(new PlayModel() { Players = "Egio x Jaco", PlayResult = "4 x 2" });
            playModelsMock.Add(new PlayModel() { Players = "Egio x Jaco", PlayResult = "2 x 5" });
            playModelsMock.Add(new PlayModel() { Players = "Egio x Jaco", PlayResult = "0 x 3" });
            playModelsMock.Add(new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 2" });
            playModelsMock.Add(new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 6" });
            playModelsMock.Add(new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 2" });

            _mapper = new ThrowingStonesGameMapper();

            _throwingStonesGameServiceMock = new ThrowingStonesGameService();
        }

        public ThrowingStonesGameServiceTest()
        {
            Setup();
        }


        [Fact]
        public void GetMatches_WithSucces_ReturnedMatches()
        {
            var playsInfosMock = _mapper.MapPlayInfos(playModelsMock);
            var matches = _throwingStonesGameServiceMock.GetMatches(playsInfosMock);

            int totalMatches = 2;

            Assert.True(matches != null);
            Assert.Equal(totalMatches, matches.Count);

            foreach ( var match in matches )
            {
                Assert.True(match.FirstPlayer != null);
                Assert.True(match.SecondPlayer != null);
            }
        }

        [Fact]
        public void GetMatches_WhenPlaysCountIsZero_ReturnedMatches()
        {
            var playerModelListMock = new List<PlayModel>();
            var playsInfosMock = _mapper.MapPlayInfos(playerModelListMock);
            var matches = _throwingStonesGameServiceMock.GetMatches(playsInfosMock);

            int totalMatches = 0;

            Assert.True(matches != null);
            Assert.Equal(totalMatches, matches.Count);
        }
    }
}