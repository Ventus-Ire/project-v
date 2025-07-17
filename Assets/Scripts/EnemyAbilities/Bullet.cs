using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public Vector3 velocity;
    public float speed;
    [SerializeField] private float lifeTime = 5f;

    private ObjectPool<Bullet> pool;
    private Rigidbody body;
    private float currentTime;
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        currentTime = lifeTime;
    }
    private void Start()
    {
        ApplyVelocity();
    }
    void Update()
    {
        UpdateLifeTime();
    }

    private void OnEnable()
    {
        ApplyVelocity();
        currentTime = lifeTime;
    }

    private void ApplyVelocity()
    {
        if (body != null)
        {
            body.linearVelocity = speed * velocity;
        }
    }
    public void SetPool(ObjectPool<Bullet> newpool)
    {
        pool = newpool;
    }
    private void ReleaseFromPool()
    {
        pool?.Release(this);
    }
    private void UpdateLifeTime()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            ReleaseFromPool();
        }
    }
}
