using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _travelSpeed = 200f;
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private float extraHorizontal = 2f;
    [SerializeField] private float extraVertical = 1f;

    [Header("Boost")]
    [SerializeField] private float _boostSpeed = 700f;
    [SerializeField] private float _boostDuration = 1f;
    [SerializeField] private float _boostCoolDown = 3f;
    [SerializeField] private KeyCode _boostKey = KeyCode.Space;
    [SerializeField] private VFXBoostColor _vfx;
    [SerializeField] FulScreenEffectController _speedVignette;
    private float _boostTimer = 0f;
    private float _cooldownTimer = 0f;
    private bool _isBoosting = false;

    [Header("Tilt Settings")]
    [SerializeField] private float _tiltAmount = 50f;            // Maximum tilt angle
    [SerializeField] private float _tiltSmoothSpeed = 6f;        // How quickly tilt interpolates
    [SerializeField] private float _noseTiltY = -0.75f;
    [SerializeField] private float _noseTiltX = 0.75f;

    [Header("Z Plane")]
    [SerializeField] private float _fixedZPlane = 4f;
    [SerializeField] private float _boostZPlane = 8f;
    [SerializeField] private float _currentZPlane;
    [SerializeField] private float _ZPlaneSmoothness = 0.33f;
    private float _zLerpTimer = 0;

    [Header("Cinemachine Dolly Cart")]
    [SerializeField] private CinemachineDollyCart _cinemachineDollyCart;

    private Camera _mainCamera;

    private Vector2 _minBounds;  // Movement bounds in local space
    private Vector2 _maxBounds;

    private Quaternion _targetRotation;

    void Start()
    {
        _mainCamera = Camera.main;
        CalculateMovementBounds();
        _targetRotation = transform.localRotation;
        _currentZPlane = _fixedZPlane;
    }

    private void Update()
    {
        HandleMovement();
        HandleSpeed();
    }

    private void HandleSpeed()
    {
        // Countdown cooldown
        if (_cooldownTimer > 0)
            _cooldownTimer -= Time.deltaTime;

        // Start boost
        if (!_isBoosting && _cooldownTimer <= 0 && Input.GetKeyDown(_boostKey))
        {
            _isBoosting = true;
            _speedVignette.ToggleSpeedVignette(true);
            _vfx.SetEvent("Boost");
            _boostTimer = _boostDuration;

            // Reset lerp timer for Z-plane IN
            _zLerpTimer = 0f;
        }

        // While boosting
        if (_isBoosting)
        {
            _boostTimer -= Time.deltaTime;

            // Speed lerp OUT of boost
            float t = 1f - (_boostTimer / _boostDuration);
            t = Mathf.Clamp01(t);

            float currentSpeed = Mathf.Lerp(_boostSpeed, _travelSpeed, t);
            _cinemachineDollyCart.m_Speed = currentSpeed;

            // Z-plane lerp IN (toward boostZ)
            _zLerpTimer += Time.deltaTime;
            float Zlerp = Mathf.Clamp01(_zLerpTimer / _ZPlaneSmoothness); // how fast Z moves IN
            _currentZPlane = Mathf.Lerp(_fixedZPlane, _boostZPlane, Zlerp);

            // End of boost
            if (_boostTimer <= 0)
            {
                _isBoosting = false;
                _speedVignette.ToggleSpeedVignette(false);
                _cooldownTimer = _boostCoolDown;

                // Reset for Z-plane OUT
                _zLerpTimer = 0f;
            }
        }
        else
        {
            // Normal speed
            _cinemachineDollyCart.m_Speed = _travelSpeed;

            // Z-plane lerp OUT (back to fixed)
            if (_currentZPlane != _fixedZPlane)
            {
                _zLerpTimer += Time.deltaTime;
                float z01 = Mathf.Clamp01(_zLerpTimer / 0.35f); // how fast Z moves OUT
                _currentZPlane = Mathf.Lerp(_boostZPlane, _fixedZPlane, z01);
            }
        }
    }


    private void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX, inputY, 0f) * _moveSpeed * Time.deltaTime;
        Vector3 newLocalPos = transform.localPosition + movement;

        newLocalPos.x = Mathf.Clamp(newLocalPos.x, _minBounds.x, _maxBounds.x);
        newLocalPos.y = Mathf.Clamp(newLocalPos.y, _minBounds.y, _maxBounds.y);

        // LERPed Z-plane
        newLocalPos.z = _currentZPlane;

        transform.localPosition = newLocalPos;

        float tiltZ = -inputX * _tiltAmount;
        float tiltX = inputY * _tiltAmount * _noseTiltY;
        float yawY = inputX * (_tiltAmount * _noseTiltX);

        _targetRotation = Quaternion.Euler(tiltX, yawY, tiltZ);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetRotation, Time.deltaTime * _tiltSmoothSpeed);
    }


    void CalculateMovementBounds()
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("Main Camera not found");
            return;
        }

        // Distance from camera to parent object's plane along camera forward
        float distance = Vector3.Dot(transform.parent.position - _mainCamera.transform.position, _mainCamera.transform.forward);

        // Get world positions of viewport corners at that distance
        Vector3 bottomLeft = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 bottomRight = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topLeft = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 topRight = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));

        // Convert world positions to local positions relative to the parent
        Transform parent = transform.parent;
        Vector3 localBL = parent.InverseTransformPoint(bottomLeft);
        Vector3 localBR = parent.InverseTransformPoint(bottomRight);
        Vector3 localTL = parent.InverseTransformPoint(topLeft);
        Vector3 localTR = parent.InverseTransformPoint(topRight);

        // Calculate min/max bounds in local space
        _minBounds = new Vector2(
            Mathf.Min(localBL.x, localBR.x, localTL.x, localTR.x),
            Mathf.Min(localBL.y, localBR.y, localTL.y, localTR.y)
        );

        _maxBounds = new Vector2(
            Mathf.Max(localBL.x, localBR.x, localTL.x, localTR.x),
            Mathf.Max(localBL.y, localBR.y, localTL.y, localTR.y)
        );

        // Movement outside bounds offset
        _minBounds.x -= extraHorizontal;
        _maxBounds.x += extraHorizontal;

        _minBounds.y -= extraVertical;
        _maxBounds.y += extraVertical;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (transform.parent == null)
            return;

        Gizmos.color = Color.green;

        // Calculate center and size for gizmo box in world space
        Vector3 center = (transform.parent.TransformPoint(_minBounds) + transform.parent.TransformPoint(_maxBounds)) * 0.5f;
        Vector3 size = new Vector3(_maxBounds.x - _minBounds.x, _maxBounds.y - _minBounds.y, 0.1f);

        Gizmos.DrawWireCube(center, size);
    }
#endif
}
