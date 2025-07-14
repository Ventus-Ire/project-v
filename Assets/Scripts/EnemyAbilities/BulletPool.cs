using UnityEngine;
using UnityEngine.Pool;
public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private GameObject bulletContainer;

    private ObjectPool<Bullet> bulletPool;


    void Start()
    {
        bulletPool = new ObjectPool<Bullet>(createFunc: InstantiatePoolBullet, actionOnGet: GetBulletFromPool, actionOnRelease: ReturnBulletToPool, defaultCapacity: 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Bullet InstantiatePoolBullet()
    {
        return Instantiate(bulletPrefab);
    }

    private void GetBulletFromPool(Bullet bullet)
    {
        if (bullet.gameObject.transform.parent == null)
        {
            if (bulletContainer == null)
            {
                bullet.transform.SetParent(transform);
            }
            else
            {
                bullet.transform.SetParent(bulletContainer.transform);
            }
        }
        bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
        bullet.SetPool(bulletPool);
        bullet.gameObject.SetActive(true);
    }
    private void InstantiateBullet()
    {
        bulletPool.Get();
    }
    private void ReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
