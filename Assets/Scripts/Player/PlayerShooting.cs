using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _muzzle;
    [SerializeField] private LaserProjectile _laserPrefab;

    [Header("Settings")]
    [SerializeField] private float _fireRate = 0.15f;
    [SerializeField] private bool _doubleLasers = false;
    [SerializeField] private float _sideOffset = 0.75f;

    private float _fireTimer = 0f;
    private bool _isHoldingFire = false;

    private void Update()
    {
        _fireTimer -= Time.deltaTime;

        if (_isHoldingFire && _fireTimer <= 0f)
        {
            SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.Blaster);
            Fire();
            _fireTimer = _fireRate;
        }
    }

    public void StartFiring()
    {
        _isHoldingFire = true;
    }

    public void StopFiring()
    {
        _isHoldingFire = false;
    }

    public void Fire()
    {
        if (GameManager.Instance.HasTwinBlasterTypeA())
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
