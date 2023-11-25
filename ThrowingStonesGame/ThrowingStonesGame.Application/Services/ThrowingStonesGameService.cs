using ThrowingStonesGame.Application.Constants;
using ThrowingStonesGame.Application.Extensions;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Domain;


namespace ThrowingStonesGame.Application.Services;

public class ThrowingStonesGameService : IThrowingStonesGameService
{
    public List<Match> GetMatches(List<PlayInfosModel> playsOnTheGame)
    {
        var matches = new List<Match>();

        for (int playIndex = 0; playIndex < playsOnTheGame.Count; playIndex++)
        {
            var plays = playsOnTheGame.GetPlaysByPlayerNames(playsOnTheGame[playIndex].PlayerNames);
            playIndex += plays.Count - 1;

            var initialPlay = plays.FirstOrDefault();

            var match = new Match(firstPlayer: new Player(initialPlay.FirstPlayerName),
                                  secondPlayer: new Player(initialPlay.SecondPlayerName, playingOutHome: true));

            foreach (var play in plays)
            {
                var stoneJumps = play.BoardStoneJumps.First();

                match.FirstPlayer.IncrementStoneJumps(stoneJumps, bonus: GetBonusStoneJumpsInThePlay(stoneJumpsCount: stoneJumps));

                stoneJumps = play.BoardStoneJumps.Last();

                match.SecondPlayer.IncrementStoneJumps(stoneJumps, bonus: GetBonusStoneJumpsInThePlay(stoneJumpsCount: stoneJumps));
            }

            match.FirstPlayer.IncrementStoneJumps(bonus: GetBonusStoneJumpsInTheMatch(stoneJumpsCountInTheMatch: match.FirstPlayer.StoneJumpsHistoricCountPerPlay));

            match.SecondPlayer.IncrementStoneJumps(bonus: GetBonusStoneJumpsInTheMatch(stoneJumpsCountInTheMatch: match.SecondPlayer.StoneJumpsHistoricCountPerPlay));

            match.ComputeWinner();

            matches.Add(match);
        }

        return matches;
    }

    public int GetBonusStoneJumpsInThePlay(int stoneJumpsCount)
    {
        var stoneJumpsCountBonus = 0;

        if (stoneJumpsCount > GameRulesConstants.StoneJumpsValueForBonus)
            stoneJumpsCountBonus = GameRulesConstants.StoneJumpBonusValue;

        return stoneJumpsCountBonus;
    }

    public int GetBonusStoneJumpsInTheMatch(List<int> stoneJumpsCountInTheMatch)
    {
        double stoneJumpingTotalBonus = 0;

        var equalsJumpStonesCount = stoneJumpsCountInTheMatch.Select(x => x).Where(x => x == stoneJumpsCountInTheMatch.Sum() / stoneJumpsCountInTheMatch.Count);

        if (equalsJumpStonesCount.Count() == stoneJumpsCountInTheMatch.Count)
            stoneJumpingTotalBonus = Math.Round(stoneJumpsCountInTheMatch.Sum() * GameRulesConstants.StoneJumpAdditionalPercentage / 100D);

        return Convert.ToInt32(stoneJumpingTotalBonus);
    }
}

