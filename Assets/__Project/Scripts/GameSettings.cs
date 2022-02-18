using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObject/Game Settings")]
public class GameSettings : ScriptableObject
{
    public bool hasIntroPlayed;
    public bool hasNextGame;
    public Vector3 lastMainMenuGamePosition;

    private void OnDisable()
    {
        hasIntroPlayed = default;
        lastMainMenuGamePosition = default;
    }
}
