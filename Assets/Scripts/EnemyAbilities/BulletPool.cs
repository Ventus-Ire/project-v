using UnityEngine;
using UnityEngine.Pool;
public class BulletPool : MonoBehaviour
{
    private static BulletPool instance;
    public static BulletPool Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("Bullet Pool Instance Missing!!!");
            }
            return instance;
        }
    }

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private GameObject bulletContainer;

    public ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }

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

        //Sets the pool of the bullet so the bullet knows what pool it belongs to
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
