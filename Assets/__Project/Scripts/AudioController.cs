using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private CircusManager circusManager;

    private void Start()
    {
        circusManager = FindObjectOfType<CircusManager>();
    }

    public void ToggleMusic()
    {
        if (circusManager == null)
        {
            return;
        }

        circusManager.MusicAudioSource.mute = !circusManager.MusicAudioSource.mute;
    }
}
