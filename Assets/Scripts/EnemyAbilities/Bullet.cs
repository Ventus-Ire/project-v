using Unity.Android.Gradle.Manifest;
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
        currentTime = lifeTime;
    }

    public void ApplyVelocity()
    {
        if (body != null)
        {
            //Gravity is overwritten because of linear velocity, need to add it later
            //might also have to turn on use gravity in rigidbody
            //velocity.y += -1f * Time.deltaTime;
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
            ApplyVelocity();
        }
        else
        {
            ReleaseFromPool();
        }
    }
}
