using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreBoardCanvas;
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
    [SerializeField]
    private TextMeshProUGUI inputErrorText;
    [SerializeField]
    private GameObject gameSettingCanvas;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        gameManager.PlayerScored += OnPlayerScored;
        gameManager.GameStarted += OnGameStarted;
        gameManager.GameEnded += OnGameEnded;

        scoreBoardCanvas.SetActive(false);
        tutorialCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);

        inputErrorText.gameObject.SetActive(false);

        gameScoreText.text = "0";
    }

    private void Update()
    {
        UpdateGameTime();
    }

    public void SaveScore(TMP_InputField input)
    {
        if (string.IsNullOrWhiteSpace(input.text))
        {
            inputErrorText.gameObject.SetActive(true);
            return;
        }

        bool hasSaved = gameManager.SaveScore(input.text);

        if (hasSaved == false)
        {
            Debug.Log("Score is already saved");
        }
    }

    private void OnGameStarted(object sender, EventArgs e)
    {
        scoreBoardCanvas.SetActive(true);
        tutorialCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameSettingCanvas.SetActive(false);
    }

    private void OnGameEnded(object sender, EventArgs e)
    {
        scoreBoardCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameSettingCanvas.SetActive(true);

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
