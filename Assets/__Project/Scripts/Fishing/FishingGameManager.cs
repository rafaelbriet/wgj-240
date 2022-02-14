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
        fishList = new List<GameObject>();

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
        float xPosition = -(fishTankSize.x / 2 - fishTankCenter.x + (fishSpacing / 2));
        
        for (float x = -fishTankSize.x / 2; x < fishTankSize.x / 2; x += fishSpacing)
        {
            xPosition += fishSpacing;
            float yPosition = -(fishTankSize.y / 2 - fishTankCenter.y + (fishSpacing / 2));

            for (float y = -fishTankSize.y / 2; y < fishTankSize.y / 2; y += fishSpacing)
            {
                yPosition += fishSpacing;
                fishList.Add(Instantiate(fishPrefab, new Vector2(xPosition, yPosition), Quaternion.identity));
            }
        }
    }
}
