using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreBoard;
    [SerializeField]
    private GameObject scorePrefab;
    [SerializeField]
    private Transform scoreBoardScrollViewContent;
    [SerializeField]
    private TextMeshProUGUI scoreBoardHeading;

    private CircusManager circusManager;

    private void Start()
    {
        circusManager = FindObjectOfType<CircusManager>();

        scoreBoard.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        if (circusManager == null)
        {
            Debug.LogError("CircusManager is not present in the scene.");
        }

        circusManager.LoadScene(sceneName);
    }

    public void CloseScoreBoard()
    {
        scoreBoard.SetActive(false);

        foreach (Transform child in scoreBoardScrollViewContent)
        {
            Destroy(child.gameObject);
        }
    }

    public void OpenScoreBoard(string gameName)
    {
        scoreBoard.SetActive(true);

        scoreBoardHeading.text = $"{gameName} Score Board";

        List<Score> scores = circusManager.Circus.GetScoresSortedByScore(gameName);

        if (scores == null)
        {
            return;
        }

        for (int i = 0; i < scores.Count; i++)
        {
            ScoreUI score = Instantiate(scorePrefab, scoreBoardScrollViewContent).GetComponent<ScoreUI>();

            score.SetContent(i + 1, scores[i].PlayerName, scores[i].Total);
        }
    }
}
