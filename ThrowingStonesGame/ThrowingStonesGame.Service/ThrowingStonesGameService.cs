using ThrowingStonesGame.Domain;
using ThrowingStonesGame.Service.Dtos;
using ThrowingStonesGame.Service.Interfaces;

namespace ThrowingStonesGame.Service;

public class ThrowingStonesGameService : IThrowingStonesGameService
{
    public List<PlayerClassification> GenerateClassification(List<PlayDto> plays)
    {
        var gameMatches = OrganizeMatchesResults(plays);

        var rankingResult = CalculatePontuation(gameMatches);

        var classificationRanking = rankingResult.OrderByDescending(x => x.TotalScore).ToList();

        return classificationRanking;
    }

    public List<MatchDto> OrganizeMatchesResults(List<PlayDto> plays)
    {
        var matches = new List<MatchDto>();

        for (var index = 0; index < plays.Count; index++)
        {
            Predicate<PlayDto> matchPredicate = (x => x.Players == plays[index].Players);

            var playsOnTheMatch = plays.FindAll(matchPredicate);

            index += playsOnTheMatch.Count;

            var match = GetMatchResult(playsOnTheMatch);

            matches.Add(match);
        }

        return matches;
    }

    public MatchDto GetMatchResult(List<PlayDto> plays)
    {
        var playerNames = plays.FirstOrDefault().Players.Split('x');
        var match = new MatchDto { Players = new List<PlayerDto>() };
        var nameIndex = 0;

        foreach (var name in playerNames)
        {
            var playsIndex = 0;

            var player = match.Players.Find(x => x.Name == name);

            for (playsIndex = 0; playsIndex < plays.Count; playsIndex++)
            {
                player ??= new PlayerDto() { Name = name };

                var stoneJumpsCount = int.Parse(plays[playsIndex].MatchResult.Split('x')[nameIndex]);

                player.StoneJumpingTotalCount += CalculateTheStoneJumpBonusInThePlay(stoneJumpsCount);
                player.StoneJumpingCountInThePlays.Add(stoneJumpsCount);

            }

            player.StoneJumpingTotalCount = CalculateTotalStoneJumpBonusInTheMatch(player.StoneJumpingCountInThePlays, player.StoneJumpingTotalCount);
            match.Players.Add(player);
            nameIndex++;
        }

        return match;
    }

    public int CalculateTheStoneJumpBonusInThePlay(int stoneJumpsCount)
    {
        var stoneJumpsValueForBonus = 10;

        if (stoneJumpsCount > stoneJumpsValueForBonus)
        {
            var stoneJumpsCountBonus = 2;
            stoneJumpsCount += stoneJumpsCountBonus;
        }

        return stoneJumpsCount;
    }

    public int CalculateTotalStoneJumpBonusInTheMatch(List<int> stoneJumpingCountInThePlays, int stoneJumpingTotalCount)
    {
        var equalsJumpStonesCount = stoneJumpingCountInThePlays.Select(x => x).Count();

        if (equalsJumpStonesCount == stoneJumpingCountInThePlays.Count)
        {
            var percentagemAdditional = 10;

            stoneJumpingTotalCount += int.Parse((stoneJumpingTotalCount * percentagemAdditional / 100).ToString("F0"));
        }

        return stoneJumpingTotalCount;

    }

    public List<PlayerClassification> CalculatePontuation(List<MatchDto> matchDtos)
    {
        List<PlayerClassification> playerClassificationList = new List<PlayerClassification>();

        foreach (var match in matchDtos)
        {
            for (var index = 0; index < match.Players.Count; index++)
            {
                var firstPlayerForCompareResult = match.Players[index];
                var secondPlayerForCompareResult = match.Players[index + 1];

                var outHomePlayerWinner = false;
                var minStoneJumpsCountAllowed = 3;

                if (firstPlayerForCompareResult.StoneJumpingTotalCount == secondPlayerForCompareResult.StoneJumpingTotalCount)
                {
                    var equalPotuation = firstPlayerForCompareResult.StoneJumpingTotalCount;

                    if (equalPotuation < minStoneJumpsCountAllowed)
                    {
                        firstPlayerForCompareResult.StoneJumpingTotalCount += -3;
                        secondPlayerForCompareResult.StoneJumpingTotalCount += -3;
                    }

                    var outHomeAdditionalPoints = 2;
                    secondPlayerForCompareResult.StoneJumpingTotalCount += outHomeAdditionalPoints;
                    outHomePlayerWinner = true;
                }

                PlayerClassification firstPlayerClassification = playerClassificationList.Find(x => x.PlayerName == firstPlayerForCompareResult.Name);

                firstPlayerClassification ??= new PlayerClassification() { PlayerName = firstPlayerForCompareResult.Name };

                PlayerClassification secondPlayerClassification = playerClassificationList.Find(x => x.PlayerName == secondPlayerForCompareResult.Name);

                secondPlayerClassification ??= new PlayerClassification() { PlayerName = firstPlayerForCompareResult.Name };

                if (outHomePlayerWinner)
                    secondPlayerClassification.TotalScore += 3;
                else
                    firstPlayerClassification.TotalScore += 3;

                playerClassificationList.Add(secondPlayerClassification);
                playerClassificationList.Add(firstPlayerClassification);

            }
        }

        return playerClassificationList;
    }
}