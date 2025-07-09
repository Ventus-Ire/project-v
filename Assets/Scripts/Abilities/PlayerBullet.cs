using UnityEngine;
using UnityEngine.Pool;
public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime = 5f;
    
    private ObjectPool<PlayerBullet> pool;
    private Rigidbody body;
    private float currentTime;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        currentTime = lifeTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ApplyVelocity();
    }

    // Update is called once per frame
    void Update()
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
    private void OnEnable()
    {
        ApplyVelocity();
        currentTime = lifeTime;
    }
    private void ApplyVelocity()
    {
        if (body != null)
        {
            body.angularVelocity = speed * transform.forward;
        }
    }

    private void ReleaseFromPool()
    {
        pool?.Release(this);
    }

}
