using ThrowingStonesGame.API.Mapping.Interfaces;
using ThrowingStonesGame.API.Models.Jsons;
using ThrowingStonesGame.Service.Dtos;

namespace ThrowingStonesGame.API.Mapping
{
    public class Mapper : IMapper
    {
        public List<PlayDto> MapperForPlayMatchDtoList(List<PlayModel> plays)
        {
            var playDtoList = new List<PlayDto>();

            for (var index = 0; index < plays.Count; index++)
            {
                var play = plays[index];
                playDtoList.Add(new PlayDto { Players = play.Players, MatchResult = play.MatchResult });
            }

            return playDtoList;
        }
    }
}
