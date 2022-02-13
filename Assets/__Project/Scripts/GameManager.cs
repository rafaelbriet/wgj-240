using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Starting, Playing, Ended}

public abstract class GameManager : MonoBehaviour
{
    [SerializeField]
    protected float gameTimeInSecond = 60f;

    public event EventHandler PlayerScored;

    public int GameScore { get; protected set; }
    public float GameTime { get; protected set; }
    public GameState CurrentGameState { get; private set; }

    private void Awake()
    {
        ChangeGameState(GameState.Starting);

        StartGame();
    }

    private void Update()
    {
        UpdateGameTime();
    }

    public abstract void StartGame();

    public abstract void StopGame();

    public abstract void Score(int amount);

    public string GetGameTimeInMinutes()
    {
        float minutes = GameTime / 60f;
        float seconds = (minutes - (int)minutes) * 60f;

        return $"{(int)minutes:00}:{seconds:00}";
    }

    protected virtual void OnPlayerScored()
    {
        PlayerScored?.Invoke(this, EventArgs.Empty);
    }

    protected void ChangeGameState(GameState state)
    {
        CurrentGameState = state;
    }

    protected void StartGameTimer()
    {
        GameTime = gameTimeInSecond;
    }

    private void UpdateGameTime()
    {
        if (CurrentGameState != GameState.Playing)
        {
            return;
        }

        GameTime -= Time.deltaTime;

        if (GameTime < 0)
        {
            StopGame();
        }
    }
}
