using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private CircusManager circusManager;

    private void Start()
    {
        circusManager = FindObjectOfType<CircusManager>();
    }

    public void LoadScene(string sceneName)
    {
        circusManager.LoadScene(sceneName);
    }
}
