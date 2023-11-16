using ThrowingStonesGame.API.Models.Jsons;
using ThrowingStonesGame.Service.Dtos;

namespace ThrowingStonesGame.API.Mapping.Interfaces
{
    public interface IMapper
    {
        public List<PlayDto> MapperForPlayMatchDtoList(List<PlayModel>? plays);
    }
}
