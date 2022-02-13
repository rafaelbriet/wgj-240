using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private int pointsAmount = 50;

    private TargetShootingManager targetShootingManager;

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void Init(TargetShootingManager manager)
    {
        targetShootingManager = manager;
    }

    public void Hit()
    {
        targetShootingManager.Score(pointsAmount);

        Destroy(gameObject);
    }
}
