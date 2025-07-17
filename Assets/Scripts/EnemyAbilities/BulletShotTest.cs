using UnityEngine;

public class BulletShotTest : MonoBehaviour
{
    [SerializeField] private float shootCooldown;
    [SerializeField] private float bulletSpeed;

    private float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            Shot(transform.position, transform.up, 1f);
            cooldownTimer += shootCooldown;
        }
    }

    private void Shot(Vector3 origin, Vector3 velocity, float speed)
    {
        Bullet bullet = BulletPool.Instance.bulletPool.Get();
        bullet.transform.position = origin;
        bullet.velocity = velocity;
        bullet.speed = speed;
    }
}
