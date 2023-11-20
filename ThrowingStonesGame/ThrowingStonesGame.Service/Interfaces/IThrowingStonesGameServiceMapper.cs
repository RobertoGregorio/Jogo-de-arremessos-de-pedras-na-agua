using ThrowingStonesGame.Application.Models;
using ThrowingStonesGame.Domain;

namespace ThrowingStonesGame.Application.Interfaces
{
    public interface IThrowingStonesGameServiceMapper
    {
        public List<Play> MapperForDomainListPlays(List<PlayModel> plays);
    }
}
