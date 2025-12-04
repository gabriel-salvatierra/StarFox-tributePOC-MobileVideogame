using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 2000f;
    [SerializeField] private float _lifeTime = 1.2f;

    private float _lifeTimer;
    private Transform _cam;

    private void Awake()
    {
        _cam = Camera.main.transform;
    }

    public void Activate()
    {
        _lifeTimer = _lifeTime;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        // Move straight into the screen (Star Fox style)
        transform.position += _cam.forward * _speed * Time.deltaTime;

        // Lifetime
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
            Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponentInParent<EnemyHealth>();
        if (enemy != null)
        {
            if (GameManager.Instance.HasTwinBlasterTypeA())
            {
                enemy.TakeDamage(3);
            }
            else
            {
                enemy.TakeDamage(1);
            }
            Deactivate();
        }
        else
        {
            var boss = other.GetComponentInParent<BossHealth>();
            if (boss != null)
            {
                if (GameManager.Instance.HasTwinBlasterTypeA())
                {
                    enemy.TakeDamage(3);
                }
                else
                {
                    enemy.TakeDamage(1);
                }
                Deactivate();
            }
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        LaserPool.Return(this);
    }
}
