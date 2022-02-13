using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircusManager : MonoBehaviour
{
    private string currentSceneName = "MainMenu";
    private string previousSceneName;

    private void Awake()
    {
        SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName)
    {
        previousSceneName = currentSceneName;
        currentSceneName = sceneName;

        var loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadOperation.completed += OnLoadSceneCompleted;
    }

    private void OnLoadSceneCompleted(AsyncOperation obj)
    {
        SceneManager.UnloadSceneAsync(previousSceneName);
    }
}
