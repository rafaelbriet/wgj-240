using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingGameManager : GameManager
{
    [SerializeField]
    private Vector2 fishTankCenter = Vector2.zero;
    [SerializeField]
    private Vector2 fishTankSize = new Vector2(5f, 5f);
    [SerializeField]
    private float fishSpacing = 1f;
    [SerializeField]
    private GameObject[] fishPrefabs;
    [SerializeField]
    private FishingPlayerController playerController;

    private List<GameObject> fishList;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(fishTankCenter, fishTankSize);
    }

    public override void Score(int amount)
    {
        GameScore += amount;

        base.OnPlayerScored();
    }

    public override void StartGame()
    {
        OnGameStarted();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        fishList = new List<GameObject>();

        SpawnFish();
        StartGameTimer();

        ChangeGameState(GameState.Playing);
    }

    public override void StopGame()
    {
        OnGameEnded();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        ChangeGameState(GameState.Ended);
    }

    public override void RestartGame()
    {
        foreach (GameObject fish in fishList)
        {
            Destroy(fish);
        }

        fishList.Clear();

        ResetGameScore();

        playerController.ResetPlayer();

        StartGame();
    }

    public void RemoveFish(GameObject fish)
    {
        fishList.Remove(fish);
        Destroy(fish);

        if (fishList.Count <= 0)
        {
            StopGame();
        }
    }

    private void SpawnFish()
    {
        float xPosition = -(fishTankSize.x / 2 - fishTankCenter.x + (fishSpacing / 2));
        
        for (float x = -fishTankSize.x / 2; x < fishTankSize.x / 2; x += fishSpacing)
        {
            xPosition += fishSpacing;
            float yPosition = -(fishTankSize.y / 2 - fishTankCenter.y + (fishSpacing / 2));

            for (float y = -fishTankSize.y / 2; y < fishTankSize.y / 2; y += fishSpacing)
            {
                yPosition += fishSpacing;
                GameObject fishToSpawn = fishPrefabs[Random.Range(0, fishPrefabs.Length)];
                fishList.Add(Instantiate(fishToSpawn, new Vector2(xPosition, yPosition), Quaternion.identity));
            }
        }
    }
}
