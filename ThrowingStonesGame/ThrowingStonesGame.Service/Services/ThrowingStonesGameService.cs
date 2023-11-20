using System.Numerics;
using ThrowingStonesGame.Application.Interfaces;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Domain;
using ThrowingStonesGame.Service.Interfaces;

namespace ThrowingStonesGame.Application.Services;

public class ThrowingStonesGameService : IThrowingStonesGameService
{
    private IThrowingStonesGameServiceMapper _mapper;

    public ThrowingStonesGameService(IThrowingStonesGameServiceMapper mapper) => _mapper = mapper;

    public List<GameResultInfos> GenerateClassification(List<PlayModel> playModelList)
    {
        var plays = _mapper.MapperForDomainListPlays(playModelList);

        var gameMatches = GetMatchesResults(plays);

        var rankingResult = CalculateGeneralPontuation(gameMatches);

        var classificationRanking = rankingResult.OrderByDescending(x => x.TotalScores).Distinct().ToList();

        return classificationRanking;
    }

    public List<Match> GetMatchesResults(List<Play> allPlaysInTheGameList)
    {
        var matchResults = new List<Match>();

        for (var index = 0; index < allPlaysInTheGameList.Count; index++)
        {
            var plays = allPlaysInTheGameList.FindAll(x => x.Players == allPlaysInTheGameList[index].Players);

            index += plays.Count;

            var matchResult = GetMatchResult(plays);

            matchResults.Add(matchResult);
        }

        return matchResults;
    }

    public Match GetMatchResult(List<Play> plays)
    {
        var playerNames = plays.FirstOrDefault().Players.Split('x');

        Match matchResult = new();
        var playerNameIndex = 0;

        foreach (var name in playerNames)
        {
            var player = matchResult.Players.Find(x => x.Name == name);

            for (var playIndex = 0; playIndex < plays.Count; playIndex++)
            {
                var isSecondPlayer = playerNames.Count() == playerNameIndex + 1;

                player ??= new Player() { Name = name.Trim(' '), PlayingAwayFromHome = isSecondPlayer };

                var stoneJumpsCount = int.Parse(plays[playIndex].MatchResult.Split('x')[playerNameIndex]);

                player.StoneJumpingTotalCountInTheMatch += stoneJumpsCount + CalculateTheStoneJumpBonusInThePlay(stoneJumpsCount);
                player.StoneJumpingCountInThePlays.Add(stoneJumpsCount);
            }

            player.StoneJumpingTotalCountInTheMatch += CalculateTotalStoneJumpBonusInTheMatch(player.StoneJumpingCountInThePlays, player.StoneJumpingTotalCountInTheMatch);
            matchResult.Players.Add(player);
            playerNameIndex++;
        }

        return matchResult;
    }

    public int CalculateTheStoneJumpBonusInThePlay(int stoneJumpsCount)
    {
        var stoneJumpsCountBonus = 0;
        var stoneJumpsValueForBonus = 10;

        if (stoneJumpsCount > stoneJumpsValueForBonus)
            stoneJumpsCountBonus = 2;

        return stoneJumpsCountBonus;
    }

    public int CalculateTotalStoneJumpBonusInTheMatch(List<int> stoneJumpingCountInThePlays, int stoneJumpingTotalCount)
    {
        int stoneJumpingTotalBonus = 0;

        var equalsJumpStonesCount = (stoneJumpingCountInThePlays.Select(x => x).Where(x => x == stoneJumpingTotalCount / stoneJumpingCountInThePlays.Count)).ToList();

        if (equalsJumpStonesCount.Count == stoneJumpingCountInThePlays.Count)
        {
            var percentageAdditional = 10;

            stoneJumpingTotalBonus = int.Parse((stoneJumpingTotalCount * percentageAdditional / 100).ToString("F0"));
        }

        return stoneJumpingTotalBonus;
    }


    public List<GameResultInfos> CalculateGeneralPontuation(List<Match> matchesResult)
    {
        List<GameResultInfos> gameResultInfos = new List<GameResultInfos>();

        foreach (var match in matchesResult)
        {
            var firstPlayer = match.Players.FirstOrDefault();
            var secondPlayer = match.Players.Last();

            var IsTie = firstPlayer.StoneJumpingTotalCountInTheMatch == secondPlayer.StoneJumpingTotalCountInTheMatch;

            if (IsTie)
            {
                var tieCasePotuation = 2;

                secondPlayer.MatchScore = tieCasePotuation;
            }
            else
            {
                var winCasePotuation = 3;

                var playerWinner = match.Players.MaxBy(x => x.StoneJumpingTotalCountInTheMatch);

                if (playerWinner.Name == firstPlayer.Name)
                    firstPlayer.MatchScore = winCasePotuation;
                else
                    secondPlayer.MatchScore = winCasePotuation;
            }

            var minStoneJumpsCountAllowed = 3;

            if (firstPlayer.StoneJumpingTotalCountInTheMatch < minStoneJumpsCountAllowed)
                firstPlayer.MatchScore = firstPlayer.MatchScore - 1;

            if (secondPlayer.StoneJumpingTotalCountInTheMatch < minStoneJumpsCountAllowed)
                secondPlayer.MatchScore = secondPlayer.MatchScore - 1;

            GameResultInfos firstPlayerClassification = gameResultInfos.Find(x => x.PlayerName == firstPlayer.Name);

            if (firstPlayerClassification == null)
            {
                firstPlayerClassification ??= new GameResultInfos() { PlayerName = firstPlayer.Name, TotalScores = firstPlayer.MatchScore };
                gameResultInfos.Add(firstPlayerClassification);
            }
            else
                firstPlayerClassification.TotalScores += firstPlayer.MatchScore;


            GameResultInfos secondPlayerClassification = gameResultInfos.Find(x => x.PlayerName == secondPlayer.Name);
            if (secondPlayerClassification == null)
            {
                secondPlayerClassification ??= new GameResultInfos() { PlayerName = secondPlayer.Name, TotalScores = secondPlayer.MatchScore };
                gameResultInfos.Add(secondPlayerClassification);
            }
            else
                secondPlayerClassification.TotalScores += secondPlayer.MatchScore;

        }

        return gameResultInfos;
    }
}