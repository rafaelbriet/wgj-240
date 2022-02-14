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
    private int fishAmount = 10;
    [SerializeField]
    private GameObject fishPrefab;

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
        fishList = new List<GameObject>(fishAmount);

        SpawnFish();

        StartGameTimer();
        ChangeGameState(GameState.Playing);
    }

    public override void StopGame()
    {
        ChangeGameState(GameState.Ended);
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
        for (int i = 0; i <= fishAmount; i++)
        {
            float xSpawnPosition = Random.Range(-fishTankSize.x / 2, fishTankSize.x / 2);
            float ySpawnPosition = Random.Range(-fishTankSize.y / 2, fishTankSize.y / 2);
            Vector2 spawnPosition = new Vector2(xSpawnPosition, ySpawnPosition);

            fishList.Add(Instantiate(fishPrefab, spawnPosition, Quaternion.identity));
        }
    }
}
