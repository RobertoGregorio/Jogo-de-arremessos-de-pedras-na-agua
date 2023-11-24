using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Application.Interfaces.Service;

public interface IThrowingStonesGameService
{
    public List<Match> GetMatches(List<PlayInfosModel> playsOnTheGame);
    public int GetBonusStoneJumpsInThePlay(int stoneJumpsCount);
    public int GetBonusStoneJumpsInTheMatch(List<int> stoneJumpsValuesInThePlays);

}
