using System;
using UnityEngine;

[System.Serializable]
public class RadialShotSettings
{
    [Header("Base Settings")]
    public int NumberOfBullets = 5;
    public float BulletSpeed = 1f;
    public float CooldownAfterShot;

    [Serializable] public enum ShotType { Simple, Radial, Spread, Sphere }
    [Header("Shot Type")]
    public ShotType shot;

    [Header("Sphere Settings")]
    public int NumberOfRings = 3;


    public float minRotation;
    public float maxRotation;
    public bool isRandom;
}
