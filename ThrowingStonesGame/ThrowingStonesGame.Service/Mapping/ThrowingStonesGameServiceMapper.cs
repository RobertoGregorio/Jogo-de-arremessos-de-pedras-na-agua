using ThrowingStonesGame.Application.Interfaces;
using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.API.Mapping
{
    public class ThrowingStonesGameServiceMapper : IThrowingStonesGameServiceMapper
    {
        public List<Play> MapperForDomainListPlays(List<PlayModel> playsModelList)
        {
            var plays = new List<Play>();

            foreach (var playModel in playsModelList)
            {
                plays.Add(new Play { Players = playModel.Players, MatchResult = playModel.MatchResult });
            }

            return plays;
        }
    }
}