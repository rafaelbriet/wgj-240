using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingGameManager : GameManager
{
    public override void Score(int amount)
    {
        GameScore += amount;

        base.OnPlayerScored();
    }

    public override void StartGame()
    {
        StartGameTimer();
        ChangeGameState(GameState.Playing);
    }

    public override void StopGame()
    {
        ChangeGameState(GameState.Ended);
    }
}
