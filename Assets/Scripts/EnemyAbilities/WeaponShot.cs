using System.Collections;
using UnityEngine;

public class WeaponShot : MonoBehaviour
{
    [SerializeField] private RadialShotPattern shotPattern;
    private bool isPatternOn = false;

    void Update()
    {
        if(isPatternOn)
        {
            return;
        }
        StartCoroutine(RunPattern());
        
    }

    private IEnumerator RunPattern()
    {
        isPatternOn= true;
        int lap = 0;

        yield return new WaitForSeconds(shotPattern.startDelay);
        while(lap < shotPattern.repetitions)
        {
            for (int i = 0; i < shotPattern.patternSettings.Length; i++)
            {
                DetermineShotType(shotPattern.patternSettings[i]);
                yield return new WaitForSeconds(shotPattern.patternSettings[i].CooldownAfterShot);
            }
            lap++;
        }

        yield return new WaitForSeconds(shotPattern.endDelay);

        isPatternOn = false;

    }

    private void DetermineShotType(RadialShotSettings settings)
    {
        switch (settings.shot)
        {
            case RadialShotSettings.ShotType.Simple:
                BulletShotType.Shot(transform.position, transform.forward, settings.BulletSpeed);
                break;

            case RadialShotSettings.ShotType.Spread:
                BulletShotType.SpreadShot(transform.position, transform.forward, settings);
                break;

            case RadialShotSettings.ShotType.Radial:
                BulletShotType.RadialShot(transform.position, transform.forward, settings);
                break;

            case RadialShotSettings.ShotType.Sphere:
                BulletShotType.SphericalShot(transform.position, transform.forward, settings);
                break;
        }
    }
}
