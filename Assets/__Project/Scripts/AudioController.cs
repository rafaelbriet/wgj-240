using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;
    [SerializeField]
    private AudioSource[] sfxAudioSources;
    [SerializeField]
    private Toggle audioToggle;

    private CircusManager circusManager;

    private void Start()
    {
        circusManager = FindObjectOfType<CircusManager>();

        if (gameSettings.isAudioMuted)
        {
            audioToggle.isOn = true;
        }
    }

    public void ToggleAllAudioSource()
    {
        gameSettings.isAudioMuted = audioToggle.isOn;

        ToggleMusic();
        ToggleSFX();
    }

    private void ToggleMusic()
    {
        if (circusManager == null)
        {
            return;
        }

        bool newMutedState = !circusManager.MusicAudioSource.mute;

        if (newMutedState == circusManager.MusicAudioSource.mute)
        {

        }

        circusManager.MusicAudioSource.mute = gameSettings.isAudioMuted;
    }

    private void ToggleSFX()
    {
        foreach (AudioSource source in sfxAudioSources)
        {
            source.mute = gameSettings.isAudioMuted;
        }
    }
}
