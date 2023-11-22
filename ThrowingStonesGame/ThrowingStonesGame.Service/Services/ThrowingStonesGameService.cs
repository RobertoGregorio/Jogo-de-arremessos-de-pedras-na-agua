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

                match.FirstPlayer.IncrementStoneJumps(stoneJumps,
                    bonus: GetStoneJumpBonus(stoneJumpsCount: stoneJumps));

                stoneJumps = play.BoardStoneJumps.Last();

                match.SecondPlayer.IncrementStoneJumps(stoneJumps,
                  bonus: GetStoneJumpBonus(stoneJumpsCount: stoneJumps));

            }

            match.FirstPlayer.IncrementStoneJumps(
                bonus: GetStoneJumpTotalCountEqualsBonus(stoneJumpingCountInThePlays: match.FirstPlayer.StoneJumpsCountHistoric,
                stoneJumpingTotalCount: match.FirstPlayer.StoneJumpsCount));

            match.SecondPlayer.IncrementStoneJumps(
                bonus: GetStoneJumpTotalCountEqualsBonus(stoneJumpingCountInThePlays: match.SecondPlayer.StoneJumpsCountHistoric,
                stoneJumpingTotalCount: match.SecondPlayer.StoneJumpsCount));

            match.ComputeWinner();
            matches.Add(match);
        }

        return matches;
    }

    private int GetStoneJumpBonus(int stoneJumpsCount)
    {
        var stoneJumpsCountBonus = 0;

        if (stoneJumpsCount > ApplicationConstants.StoneJumpsValueForBonus)
            stoneJumpsCountBonus = 2;

        return stoneJumpsCountBonus;
    }

    private int GetStoneJumpTotalCountEqualsBonus(List<int> stoneJumpingCountInThePlays, int stoneJumpingTotalCount)
    {
        double stoneJumpingTotalBonus = 0;

        var equalsJumpStonesCount = stoneJumpingCountInThePlays.Select(x => x).Where(x => x == stoneJumpingTotalCount / stoneJumpingCountInThePlays.Count);

        if (equalsJumpStonesCount.Count() == stoneJumpingCountInThePlays.Count)
            stoneJumpingTotalBonus = Math.Round(stoneJumpingTotalCount * ApplicationConstants.StoneJumpAdditionalPercentage / 100D);

        return Convert.ToInt32(stoneJumpingTotalBonus);
    }
}

