using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObject/Game Settings")]
public class GameSettings : ScriptableObject
{
    public bool isAudioMuted;
    public bool hasIntroPlayed;
    public bool hasNextGame;
    public Vector3 lastMainMenuGamePosition;

    private void OnDisable()
    {
        Debug.Log("GameSettings disabled");
        isAudioMuted = default;
        hasIntroPlayed = default;
        lastMainMenuGamePosition = default;
    }
}
