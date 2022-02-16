using System;
using System.Collections.Generic;

[Serializable]
public class GameScores
{
    public string GameName;
    public List<Score> Scores = new List<Score>();

    public GameScores(string gameName)
    {
        GameName = gameName;
    }
}
