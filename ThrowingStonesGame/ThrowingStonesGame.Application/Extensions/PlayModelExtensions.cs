using ThrowingStonesGame.Application.Models;

namespace ThrowingStonesGame.Application.Extensions;

public static class PlayModelExtensions
{
    public static List<PlayInfosModel> GetPlaysByPlayerNames(this List<PlayInfosModel> playModels, string playerNames) => playModels.FindAll(play => play.PlayerNames == playerNames);
}
