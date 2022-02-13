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

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        gameManager.PlayerScored += OnPlayerScored;
    }

    private void Update()
    {
        UpdateGameTime();
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
