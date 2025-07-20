using UnityEngine;

[System.Serializable]
public class RadialShotSettings
{
    public int NumberOfBullets = 5;
    public float BulletSpeed = 1f;
    public float CooldownAfterShot;
    public int NumberOfRings = 3;


    public float minRotation;
    public float maxRotation;
    public bool isRandom;
}
