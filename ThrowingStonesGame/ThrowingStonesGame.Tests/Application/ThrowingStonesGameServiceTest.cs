using NSubstitute;
using ThrowingStonesGame.API.Mapping;
using ThrowingStonesGame.Application.Interfaces;
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

        [Fact]
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


        [Fact]
        public void GetMatches_WithSucces_ReturnedMatchesList()
        {
            Setup();
            var playsInfosMock = _mapper.MapPlayInfos(playModelsMock);
            var matches = _throwingStonesGameServiceMock.GetMatches(playsInfosMock);

            Assert.Equal(2, matches.Count);
        }

        [Fact]
        public void GetMatches_WhenPlaysMocksNull_ReturnNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => new ThrowingStonesGameService().GetMatches(null));
        }

        [Fact]
        public void GetMatches_WhenPlaysMocksPlayerNamesIsNull_ReturnNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => new ThrowingStonesGameService().GetMatches(null));
        }
    }
}