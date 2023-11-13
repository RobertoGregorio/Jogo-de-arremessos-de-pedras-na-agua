using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThrowingStonesGame.ThrowingStonesGame.Domain
{
    public class ThrowingStonesGameService : IThrowingStonesGameService
    {
        private List<string> _players { get; set; }

        ThrowingStonesGameService(List<string> players)
        {
            _players = players
        }

        private 
    }
}