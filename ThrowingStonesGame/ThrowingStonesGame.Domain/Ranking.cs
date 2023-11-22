namespace ThrowingStonesGame.Domain;

public class Ranking
{
    public List<PlayerRanking> GeneralClassification { get; set; }

    public Ranking()
    {
        GeneralClassification = new List<PlayerRanking>();
    }

    public PlayerRanking AddIfNotExistPlayerRanking(string playerName)
    {
        PlayerRanking playerRanking = GeneralClassification.Find(x => x.Name == playerName);
        if (playerRanking == null)
        {
            playerRanking = new PlayerRanking(playerName);
            GeneralClassification.Add(playerRanking);
        }

        return playerRanking;
    }

}