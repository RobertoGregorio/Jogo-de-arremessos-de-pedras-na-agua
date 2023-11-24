using ThrowingStonesGame.API.Mapping;
using ThrowingStonesGame.Application.Interfaces.Mapping;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Tests.Application;

public class ThrowingStonesGameMapperTest
{
    private List<PlayModel> playModelsMock = new List<PlayModel>();
    private IThrowingStonesGameMapper _mapper;


    public ThrowingStonesGameMapperTest()
    {
        playModelsMock = new List<PlayModel>()
        {
            {new PlayModel() { Players = "Egio x Jaco", PlayResult = "4 x 2" }},
            {new PlayModel() { Players = "Egio x Jaco", PlayResult = "2 x 5" }},
            {new PlayModel() { Players = "Egio x Jaco", PlayResult = "0 x 3" }},
            {new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 2" }},
            {new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 6" }},
            {new PlayModel() { Players = "Jaco x Egio", PlayResult = "2 x 2" }}
        };

        _mapper = new ThrowingStonesGameMapper();
    }


    [Fact]
    public void MapperPlayInfos_WithSucces_ReturnedPlayInfos()
    {
        //Arrange
        int matchesTotalCount = 6;
        int scoreBoardTotalLenght = 2;

        //Act
        var playsInfosMock = _mapper.MapPlayInfos(playModelsMock);


        //Assertion
        Assert.True(playsInfosMock != null);
        Assert.Equal(matchesTotalCount, playsInfosMock.Count);

        foreach (var playInfo in playsInfosMock)
        {
            Assert.True(!string.IsNullOrEmpty(playInfo.PlayerNames));
            Assert.True(!string.IsNullOrEmpty(playInfo.FirstPlayerName));
            Assert.True(!string.IsNullOrEmpty(playInfo.SecondPlayerName));
            Assert.True(playInfo.BoardStoneJumps.Length == scoreBoardTotalLenght);
        }
    }

    [Fact]
    public void MapperPlayInfos_WhenPlaysIsZero_ReturnedPlayInfosCountZero()
    {
        //Arrange
        int playsInfosTotal = 0;

        //Act
        var playsInfosMock = _mapper.MapPlayInfos(new List<PlayModel>());

        //Assertion
        Assert.True(playsInfosMock != null);
        Assert.Equal(playsInfosTotal, playsInfosMock.Count);

    }

    [Fact]
    public void MapperRankingModel_WithSuccess_ReturnedRankingModel()
    {

        //Arrange
        var firstPlayerClassification = new PlayerRanking(name: "Egio")
        {
            RankingPosition = 1,
            TotalScores = 18,
            WinsCount = 6,
            TotalBonusCount = 2,
            InterestPointsCount = 0,
        };

        var secondPlayerClassification = new PlayerRanking(name: "Jaco")
        {
            RankingPosition = 1,
            TotalScores = 6,
            WinsCount = 2,
            TotalBonusCount = 0,
            InterestPointsCount = 0,
        };

        var thirdPlayerClassification = new PlayerRanking(name: "Caio")
        {
            RankingPosition = 3,
            TotalScores = 0,
            WinsCount = 0,
            TotalBonusCount = 0,
            InterestPointsCount = 0,
        };

        var ranking = new Ranking();
        ranking.GeneralClassification.Add(firstPlayerClassification);
        ranking.GeneralClassification.Add(secondPlayerClassification);
        ranking.GeneralClassification.Add(thirdPlayerClassification);


        //Act
        var rankingModel = _mapper.MapRankingModel(ranking);


        //Assertion
        Assert.True(rankingModel != null);
        Assert.Equal(rankingModel.PlayerRankingModels.Count, ranking.GeneralClassification.Count);
        Assert.Equal(rankingModel.PlayerRankingModels.First().RankingPosition, firstPlayerClassification.RankingPosition);
        Assert.Equal(rankingModel.PlayerRankingModels[1].RankingPosition, secondPlayerClassification.RankingPosition);
        Assert.Equal(rankingModel.PlayerRankingModels.Last().RankingPosition, thirdPlayerClassification.RankingPosition);
    }

    [Fact]
    public void MapperRankingModel_WhenRakingClassificationCountIsZero_ReturnedRankingModel()
    {

        //Arrange
        var ranking = new Ranking();
        var expectedTotalCount = 0;

        //Act
        var rankingModel = _mapper.MapRankingModel(ranking);


        //Assertion
        Assert.True(rankingModel != null);
        Assert.Equal(rankingModel.PlayerRankingModels.Count, expectedTotalCount);
    }
}