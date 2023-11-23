using ThrowingStonesGame.API.Mapping;
using ThrowingStonesGame.Application.Interfaces.Mapping;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Application.Services;

namespace ThrowingStonesGame.Tests.Application;

public class ThrowingStonesGameMapperTest
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

    public ThrowingStonesGameMapperTest()
    {
        Setup();
    }


    [Fact]
    public void MapPlayInfos_WithSucces_ReturnedPlayInfos()
    {
        var playsInfosMock = _mapper.MapPlayInfos(playModelsMock);

        int matchesTotalCount = 6;
        int scoreBoardTotalLenght = 2;


        Assert.True(playsInfosMock != null);
        Assert.Equal(matchesTotalCount, playsInfosMock.Count);

        foreach (var playInfo in playsInfosMock)
        {
            Assert.True(!string.IsNullOrEmpty(playInfo.PlayerNames));
            Assert.True(!string.IsNullOrEmpty(playInfo.FirstPlayerName));
            Assert.True(!string.IsNullOrEmpty(playInfo.SecondPlayerName));
            Assert.True(playInfo.BoardStoneJumps.Length ==  scoreBoardTotalLenght);
        }
    }

    [Fact]
    public void MapPlayInfos_WhenPlaysWasEmpty_ReturnedPlayInfos()
    {
        var playsInfosMock = _mapper.MapPlayInfos(new List<PlayModel>());

        int playsInfosTotal = 0;
        int scoreBoardTotalLenght = 0;

        Assert.True(playsInfosMock != null);
        Assert.Equal(playsInfosTotal, playsInfosMock.Count);

        foreach (var playInfo in playsInfosMock)
        {
            Assert.True(string.IsNullOrEmpty(playInfo.PlayerNames));
            Assert.True(string.IsNullOrEmpty(playInfo.FirstPlayerName));
            Assert.True(string.IsNullOrEmpty(playInfo.SecondPlayerName));
           
        }
    }
}
