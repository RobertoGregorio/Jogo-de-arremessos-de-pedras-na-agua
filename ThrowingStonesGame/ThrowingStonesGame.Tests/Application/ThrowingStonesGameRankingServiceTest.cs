using ThrowingStonesGame.API.Mapping;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Application.Services;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Tests.Application;

public class ThrowingStonesGameRankingServiceTest
{
    private List<PlayInfosModel> _playInfosListMock;
    private IThrowingStonesGameRankingService _throwingStonesGameRankingService;
    private IThrowingStonesGameService _throwingStonesGameService;


    public ThrowingStonesGameRankingServiceTest()
    {

        var playModelListMock = new List<PlayModel>()
        {
            {new PlayModel() { Players = "Egio x Jaco", PlayResult = "4 x 2" }},
            {new PlayModel() { Players = "Egio x Jaco", PlayResult = "2 x 5" }},
            {new PlayModel() { Players = "Egio x Jaco", PlayResult = "0 x 3" }},
            {new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 2" }},
            {new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 6" }},
            {new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 2" }}
        };

        _playInfosListMock = new ThrowingStonesGameMapper().MapPlayInfos(playModelListMock);

        _throwingStonesGameRankingService = new ThrowingStonesGameRankingService();
        _throwingStonesGameService = new ThrowingStonesGameService();
    }


    [Fact]
    public void GenerateRanking_WithSuccess_ReturnedValidRanking()
    {
        //Arrange
        List<Match> matchesListMock = _throwingStonesGameService.GetMatches(_playInfosListMock);

        //Act 
        var ranking = _throwingStonesGameRankingService.GenerateRanking(matchesListMock);

        //Assertion
        Assert.True(ranking != null);
        Assert.True(ranking.GeneralClassification != null);
        Assert.True(ranking.GeneralClassification.Count > 0);

        foreach (PlayerRanking player in ranking.GeneralClassification)
        {
            Assert.True(!string.IsNullOrEmpty(player.Name));
            Assert.True(player.RankingPosition > 0);
        }
    }

    [Fact]
    public void GenerateRanking_WhenPlayInfosCountIsZero_ReturnedEmptyRanking()
    {
        //Arrange
        List<Match> matchesListMock = _throwingStonesGameService.GetMatches(new List<PlayInfosModel>());

        //Act 
        var ranking = _throwingStonesGameRankingService.GenerateRanking(matchesListMock);

        //Assertion
        Assert.True(ranking != null);
        Assert.True(ranking.GeneralClassification != null);
    }
}
