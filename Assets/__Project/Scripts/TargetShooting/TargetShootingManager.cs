using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShootingManager : GameManager
{
    [SerializeField]
    private Transform topTargetSpawner;
    [SerializeField]
    private Transform bottomTargetSpawner;
    [SerializeField]
    private GameObject targetPrefab;
    [SerializeField]
    private float timeBetweenTargetSpawn = 2f;

    public override void StartGame()
    {
        OnGameStarted();

        StartCoroutine(SpawnTarget());

        StartGameTimer();
        ChangeGameState(GameState.Playing);
    }

    public override void StopGame()
    {
        OnGameEnded();

        StopAllCoroutines();

        ChangeGameState(GameState.Ended);
    }

    public override void Score(int amount)
    {
        GameScore += amount;

        base.OnPlayerScored();
    }

    private IEnumerator SpawnTarget()
    {
        float timeToNextTarget = timeBetweenTargetSpawn;

        while (true)
        {
            timeToNextTarget -= Time.deltaTime;

            if (timeToNextTarget <= 0f)
            {
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(targetPrefab, topTargetSpawner.position, Quaternion.identity).GetComponent<Target>().Init(this);
                }
                else
                {
                    Instantiate(targetPrefab, bottomTargetSpawner.position, Quaternion.identity).GetComponent<Target>().Init(this);
                }

                timeToNextTarget = timeBetweenTargetSpawn;
            }

            yield return null;
        }
    }

    public override void RestartGame()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject target in targets)
        {
            Destroy(target);
        }

        ResetGameScore();

        StartGame();
    }
}
