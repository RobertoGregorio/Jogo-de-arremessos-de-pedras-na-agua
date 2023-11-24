namespace ThrowingStonesGame.Application.Models;

public class PlayInfosModel
{
    public PlayInfosModel(string playersNames, string playResult)
    {
        PlayerNames = playersNames;

        var boardStoneJumps = playResult.Split('x').ToList();
        BoardStoneJumps = new int[] { int.Parse(boardStoneJumps.FirstOrDefault()), int.Parse(boardStoneJumps.Last()) };

        var names = PlayerNames.Split('x');

        FirstPlayerName = names.FirstOrDefault().Trim(' ');
        SecondPlayerName = names.Last().Trim(' ');
    }

    public string? PlayerNames { get; set; }
    public string? FirstPlayerName { get; set; }
    public string? SecondPlayerName { get; set; }
    public int[] BoardStoneJumps { get; set; }
}
