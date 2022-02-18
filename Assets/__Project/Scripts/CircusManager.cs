using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircusManager : MonoBehaviour
{
    private readonly string saveFileName = "Circus";
    private readonly string targetShootingName = "Target Shooting";
    private readonly string fishingName = "Fishing";

    [SerializeField]
    private AudioSource musicAudioSource;

    private string currentSceneName = "MainMenu";
    private string previousSceneName;
    private Circus circus;

    public AudioSource MusicAudioSource => musicAudioSource;
    public Circus Circus => circus;

    private void Awake()
    {
        SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        LoadCircus();
    }

    public void SaveCircus()
    {
        string json = JsonUtility.ToJson(Circus);
        PlayerPrefs.SetString(saveFileName, json);
        Debug.Log("Circus saved: " + json);
    }

    public void LoadCircus()
    {
        string json = PlayerPrefs.GetString(saveFileName);

        if (string.IsNullOrWhiteSpace(json) == false)
        {
            circus = JsonUtility.FromJson<Circus>(json);
            Debug.Log("Circus loaded: " + json);
            return;
        }

        circus = new Circus();

        circus.GameScores = new List<GameScores>()
            {
                new GameScores(targetShootingName),
                new GameScores(fishingName),
            };
    }

    public void SaveScore(string gameName, string playeName, int gameScore)
    {
        Circus.AddGameScores(gameName, playeName, gameScore);
        SaveCircus();
    }

    public void LoadScene(string sceneName)
    {
        previousSceneName = currentSceneName;
        currentSceneName = sceneName;

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadOperation.completed += OnLoadSceneCompleted;
    }

    private void OnLoadSceneCompleted(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentSceneName));

        SceneManager.UnloadSceneAsync(previousSceneName);
    }
}
