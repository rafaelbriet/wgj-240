using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;
    [SerializeField]
    private GameObject scoreBoard;
    [SerializeField]
    private GameObject scorePrefab;
    [SerializeField]
    private Transform scoreBoardScrollViewContent;
    [SerializeField]
    private TextMeshProUGUI scoreBoardHeading;
    [SerializeField]
    private Transform targetShootingGame;
    [SerializeField]
    private Transform fishingGame;
    [SerializeField]
    private float introDuration = 2f;
    [SerializeField]
    private float logoDuration = 3f;
    [SerializeField]
    private float moveToGameDuration = 3f;
    [SerializeField]
    private GameObject nextGameButton;
    [SerializeField]
    private GameObject previousGameButton;
    [SerializeField]
    private Camera mainCamera;

    private CircusManager circusManager;
    private bool isCameraMoving;
    private bool isIntro = true;

    private void Start()
    {
        circusManager = FindObjectOfType<CircusManager>();

        isIntro = !gameSettings.hasIntroPlayed;

        scoreBoard.SetActive(false);
        nextGameButton.SetActive(false);
        previousGameButton.SetActive(false);

        if (gameSettings.hasIntroPlayed)
        {
            Debug.Log("hasIntroPlayed: " + gameSettings.hasIntroPlayed);
            mainCamera.transform.position = new Vector3(gameSettings.lastMainMenuGamePosition.x, gameSettings.lastMainMenuGamePosition.y, mainCamera.transform.position.z);

            if (gameSettings.hasNextGame)
            {
                nextGameButton.SetActive(true);
            }
            else
            {
                previousGameButton.SetActive(true);
            }
        }
        else
        {
            Debug.Log("hasIntroPlayed: " + gameSettings.hasIntroPlayed);
            StartCoroutine(MoveCameraTo(targetShootingGame.position, introDuration));
            gameSettings.hasIntroPlayed = true;
            gameSettings.hasNextGame = true;
            gameSettings.lastMainMenuGamePosition = targetShootingGame.position;
        }
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

        scoreBoardHeading.text = $"{gameName} Scoreboard";

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

    public void MoveCameraToNextGame()
    {
        nextGameButton.SetActive(false);
        previousGameButton.SetActive(false);
        StartCoroutine(MoveCameraTo(fishingGame.position, moveToGameDuration, () => previousGameButton.SetActive(true)));
        gameSettings.lastMainMenuGamePosition = fishingGame.position;
        gameSettings.hasNextGame = false;
    }

    public void MoveCameraToPreviousGame()
    {
        nextGameButton.SetActive(false);
        previousGameButton.SetActive(false);
        StartCoroutine(MoveCameraTo(targetShootingGame.position, moveToGameDuration, () => nextGameButton.SetActive(true)));
        gameSettings.lastMainMenuGamePosition = targetShootingGame.position;
        gameSettings.hasNextGame = true;
    }

    private IEnumerator MoveCameraTo(Vector3 destination, float duration, Action action)
    {
        yield return StartCoroutine(MoveCameraTo(destination, duration));

        action?.Invoke();
    }

    private IEnumerator MoveCameraTo(Vector3 destination, float duration)
    {
        if (isCameraMoving)
        {
            yield break;
        }

        if (isIntro)
        {
            yield return new WaitForSeconds(logoDuration);
        }

        isCameraMoving = true;
        float timeElapsed = 0;
        float timeLerpStarted = Time.time;
        Vector3 originalPosition = Camera.main.transform.position;
        destination.z = originalPosition.z;

        while (timeElapsed < duration)
        {
            float timeSinceLerpStarted = Time.time - timeLerpStarted;
            float percentComplete = timeSinceLerpStarted / duration;

            mainCamera.transform.position = Vector3.Lerp(originalPosition, destination, percentComplete);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        if (isIntro)
        {
            nextGameButton.SetActive(true);
        }

        isCameraMoving = false;
        isIntro = false;
    }
}
