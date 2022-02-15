using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gameScoreText;
    [SerializeField]
    private TextMeshProUGUI gameTimeText;
    [SerializeField]
    private GameObject tutorialCanvas;
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private TextMeshProUGUI gameHighscoreText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        gameManager.PlayerScored += OnPlayerScored;
        gameManager.GameStarted += OnGameStarted;
        gameManager.GameEnded += OnGameEnded;

        tutorialCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    private void Update()
    {
        UpdateGameTime();
    }

    private void OnGameStarted(object sender, EventArgs e)
    {
        tutorialCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    private void OnGameEnded(object sender, EventArgs e)
    {
        gameOverCanvas.SetActive(true);

        gameHighscoreText.text = gameManager.GameScore.ToString();
    }

    private void OnPlayerScored(object sender, EventArgs e)
    {
        UpdateGameScore();
    }

    private void UpdateGameScore()
    {
        gameScoreText.text = gameManager.GameScore.ToString();
    }

    private void UpdateGameTime()
    {
        gameTimeText.text = gameManager.GetGameTimeInMinutes();
    }
}
