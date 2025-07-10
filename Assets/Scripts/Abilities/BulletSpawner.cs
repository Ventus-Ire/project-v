using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private PlayerBullet bulletPrefab;

    [SerializeField] int bulletActive;
    [SerializeField] int bulletInactive;

    private ObjectPool<PlayerBullet> bulletPool;
    private bool canShoot;
    public bool CanShoot
    {
        get => canShoot;
        set 
        { 
            canShoot = value;
            if(canShoot)
            {
                StartCoroutine(BulletShot(.1f));
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }

    void Start()
    {
        bulletPool = new ObjectPool<PlayerBullet>(createFunc: InstiatePoolBullet, actionOnGet: GetBulletFromPool, actionOnRelease: ReturnBulletToPool, defaultCapacity: 10, maxSize: 100);
    }


    void Update()
    {
        bulletActive = bulletPool.CountActive;
        bulletInactive = bulletPool.CountInactive;
        if(Input.GetKeyDown(KeyCode.C))
        {
            CanShoot = true;
        }
        if(Input.GetKeyDown(KeyCode.V)) 
        { 
            CanShoot = false;
        } 
    }

    private PlayerBullet InstiatePoolBullet()
    {
        return Instantiate(bulletPrefab);
    }

    private void GetBulletFromPool(PlayerBullet bullet)
    {
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetPool(bulletPool);
        bullet.gameObject.SetActive(true);
    }
    private void ReturnBulletToPool(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void InstantiateBullet()
    {
        bulletPool.Get();
    }

    private IEnumerator BulletShot(float rateOfFire)
    {
        while (canShoot)
        {
            InstantiateBullet();
            yield return new WaitForSeconds(rateOfFire);
        }
        yield return null;

    }
}
