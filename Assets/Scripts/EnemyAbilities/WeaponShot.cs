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
                BulletShotType.SphericalShot(transform.position, transform.forward, shotPattern.patternSettings[i]);
                yield return new WaitForSeconds(shotPattern.patternSettings[i].CooldownAfterShot);
            }
            lap++;
        }

        yield return new WaitForSeconds(shotPattern.endDelay);

        isPatternOn = false;

    }

    private void DetermineShotType()
    {

    }
}
