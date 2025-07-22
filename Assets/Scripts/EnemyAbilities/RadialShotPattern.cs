using UnityEngine;

[CreateAssetMenu(fileName = "RadialShotPattern", menuName = "Bullet Patterns/RadialShotPattern")]
public class RadialShotPattern : ScriptableObject
{
    public int repetitions;
    public float startDelay;
    public float endDelay;
    public RadialShotSettings[] patternSettings;
}
