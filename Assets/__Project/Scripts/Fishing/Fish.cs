using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private int pointsAmount = 50;

    public int PointsAmount { get => pointsAmount; }
}
