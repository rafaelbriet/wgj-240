using System;

[Serializable]
public class Score
{
    public string PlayerName;
    public int Total;

    public Score(string playerName, int total)
    {
        PlayerName = playerName;
        Total = total;
    }
}
