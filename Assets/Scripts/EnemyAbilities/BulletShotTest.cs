using UnityEngine;

public class BulletShotTest : MonoBehaviour
{
    [SerializeField] private RadialShotSettings settings;

    private float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            BulletShotType.FixedRadiusShot(transform.position, transform.forward, settings);
            cooldownTimer += settings.CooldownAfterShot;
        }
    }

    
}
