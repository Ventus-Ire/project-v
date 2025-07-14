using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private GameObject bulletContainer;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] int bulletActive; //TODO: Delete
    [SerializeField] int bulletInactive; //TODO: Delete

    private ObjectPool<PlayerBullet> bulletPool;
    private bool canShoot;
    public bool CanShoot
    {
        get => canShoot;
        set 
        { 
            if(canShoot != value)
            {
                canShoot = value;
                if (canShoot)
                {
                    StartCoroutine(BulletShot(fireRate));
                }
                else
                {
                    StopAllCoroutines();
                }
            }
            
        }
    }

    void Start()
    {
        bulletPool = new ObjectPool<PlayerBullet>(createFunc: InstantiatePoolBullet, actionOnGet: GetBulletFromPool, actionOnRelease: ReturnBulletToPool, defaultCapacity: 10, maxSize: 100);
    }

    //TODO: Delete
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

    private PlayerBullet InstantiatePoolBullet()
    {
        return Instantiate(bulletPrefab);
    }

    private void GetBulletFromPool(PlayerBullet bullet)
    {
        if (bullet.gameObject.transform.parent == null)
        {
            bullet.transform.SetParent(bulletContainer.transform);
        }
        bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
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

    //InstantiateBullet() gets called when you want to "fire" a bullet
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
