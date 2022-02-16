using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Circus
{
    public List<GameScores> GameScores = new List<GameScores>();

    public void AddGameScores(string gameName, string playerName, int score)
    {
        GameScores scores = GetGameScores(gameName);

        if (scores == null)
        {
            Debug.LogError($"Could not find a game with the name of {gameName}");
            return;
        }

        scores.Scores.Add(new Score(playerName, score));
    }

    public GameScores GetGameScores(string gameName)
    {
        GameScores scores = GameScores.Find(x => x.GameName == gameName);

        return scores;
    }
}
