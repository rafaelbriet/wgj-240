using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] sfxAudioSources;

    private CircusManager circusManager;

    private void Start()
    {
        circusManager = FindObjectOfType<CircusManager>();
    }

    public void ToggleAllAudioSource()
    {
        ToggleMusic();
        ToggleSFX();
    }

    public void ToggleMusic()
    {
        if (circusManager == null)
        {
            return;
        }

        circusManager.MusicAudioSource.mute = !circusManager.MusicAudioSource.mute;
    }

    public void ToggleSFX()
    {
        foreach (AudioSource source in sfxAudioSources)
        {
            source.mute = !source.mute;
        }
    }
}
