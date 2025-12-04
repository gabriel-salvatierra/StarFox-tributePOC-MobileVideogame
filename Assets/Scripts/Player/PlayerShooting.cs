using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform _muzzle;    
    [SerializeField] private LaserProjectile _laserPrefab;

    [Header("Settings")]
    [SerializeField] private KeyCode _shootingKey = KeyCode.Mouse0;
    [SerializeField] private float _fireRate = 0.15f;
    [SerializeField] private bool _doubleLasers = false;
    [SerializeField] private float _sideOffset = 0.75f;

    private float _fireTimer = 0f;

    private void Update()
    {
        _fireTimer -= Time.deltaTime;

        if (Input.GetKey(_shootingKey) && _fireTimer <= 0f)
        {
            Fire();
            _fireTimer = _fireRate;
        }
    }

    private void Fire()
    {
        if (_doubleLasers)
        {
            SpawnLaser(_muzzle.position + transform.right * _sideOffset);
            SpawnLaser(_muzzle.position - transform.right * _sideOffset);
        }
        else
        {
            SpawnLaser(_muzzle.position);
        }
    }

    private void SpawnLaser(Vector3 position)
    {
        LaserProjectile laser = LaserPool.Get(_laserPrefab);
        laser.transform.position = position;
        laser.transform.rotation = _muzzle.rotation;
        laser.Activate();
    }
}
