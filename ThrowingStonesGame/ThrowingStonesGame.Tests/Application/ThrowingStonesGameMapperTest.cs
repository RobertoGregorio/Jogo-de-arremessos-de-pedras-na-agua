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
        var playsInfosMock = _mapper.MapPlayInfosList(playModelsMock);


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
        var playsInfosMock = _mapper.MapPlayInfosList(new List<PlayModel>());

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
            TotalWins = 6,
            TotalStoneJumpsBonusCount = 2,
            TotalPunishmentPoints = 0,
        };

        var secondPlayerClassification = new PlayerRanking(name: "Jaco")
        {
            RankingPosition = 1,
            TotalScores = 6,
            TotalWins = 2,
            TotalStoneJumpsBonusCount = 0,
            TotalPunishmentPoints = 0,
        };

        var thirdPlayerClassification = new PlayerRanking(name: "Caio")
        {
            RankingPosition = 3,
            TotalScores = 0,
            TotalWins = 0,
            TotalStoneJumpsBonusCount = 0,
            TotalPunishmentPoints = 0,
        };

        var ranking = new Ranking();
        ranking.GeneralClassification.Add(firstPlayerClassification);
        ranking.GeneralClassification.Add(secondPlayerClassification);
        ranking.GeneralClassification.Add(thirdPlayerClassification);


        //Act
        var rankingModel = _mapper.MapRankingModel(ranking);


        //Assertion
        Assert.True(rankingModel != null);
        Assert.Equal(rankingModel.PlayerRankingModelList.Count, ranking.GeneralClassification.Count);
        Assert.Equal(rankingModel.PlayerRankingModelList.First().RankingPosition, firstPlayerClassification.RankingPosition);
        Assert.Equal(rankingModel.PlayerRankingModelList[1].RankingPosition, secondPlayerClassification.RankingPosition);
        Assert.Equal(rankingModel.PlayerRankingModelList.Last().RankingPosition, thirdPlayerClassification.RankingPosition);
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
        Assert.Equal(rankingModel.PlayerRankingModelList.Count, expectedTotalCount);
    }
}