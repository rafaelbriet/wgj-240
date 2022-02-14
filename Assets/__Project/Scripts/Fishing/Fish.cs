using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private int pointsAmount = 50;
    [SerializeField]
    private float rotateSpeed = 5f;
    [SerializeField]
    private float radius = 0.5f;
    [SerializeField]
    private float startingAngleOffset = 5f;

    private float angle;
    private Vector3 startingPosition;

    public bool IsHooked { get; set; }
    public int PointsAmount { get => pointsAmount; }

    private void Awake()
    {
        startingPosition = transform.position;
        angle = Random.Range(-startingAngleOffset, startingAngleOffset);
    }

    private void Update()
    {
        if (IsHooked)
        {
            return;
        }

        angle += rotateSpeed * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * radius;
        transform.position = startingPosition + offset;
    }
}
