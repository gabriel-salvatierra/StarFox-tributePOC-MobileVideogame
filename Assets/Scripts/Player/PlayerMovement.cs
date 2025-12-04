using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input / Controller")]
    [SerializeField] private Controller _controller;   //  AxisController or KeyboardController

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 50f;   // side movement speed
    [SerializeField] private float extraHorizontal = 2f;
    [SerializeField] private float extraVertical = 1f;

    [Header("Travel Speed (Auto forward)")]
    [SerializeField] private float _travelSpeed = 200f;

    [Header("Boost")]
    [SerializeField] private float _boostSpeed = 700f;
    [SerializeField] private float _boostDuration = 1f;
    [SerializeField] private float _boostCoolDown = 3f;
    [SerializeField] private VFXBoostColor _vfx;
    [SerializeField] private FulScreenEffectController _speedVignette;

    private float _boostTimer = 0f;
    private float _cooldownTimer = 0f;
    private bool _boostButtonPressed = false;
    private bool _isBoosting = false;

    [Header("Tilt Settings")]
    [SerializeField] private float _tiltAmount = 50f;
    [SerializeField] private float _tiltSmoothSpeed = 6f;
    [SerializeField] private float _noseTiltY = -0.75f;
    [SerializeField] private float _noseTiltX = 0.75f;

    [Header("Z Plane")]
    [SerializeField] private float _fixedZPlane = 4f;
    [SerializeField] private float _boostZPlane = 8f;
    [SerializeField] private float _ZPlaneSmoothness = 0.33f;
    private float _currentZPlane;
    private float _zLerpTimer = 0;

    [Header("Cinemachine Dolly Cart")]
    [SerializeField] private CinemachineDollyCart _cinemachineDollyCart;

    private Camera _mainCamera;

    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    private Quaternion _targetRotation;

    private void Start()
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

    // ----------------------------------------------------------
    // MOVEMENT — uses CONTROLLER input (AxisController or Keyboard)
    // ----------------------------------------------------------
    private void HandleMovement()
    {
        // GET UI/Keyboard joystick input
        Vector3 input = _controller.GetMovementInput();
        float inputX = input.x;
        float inputY = input.z;

        Vector3 movement = new Vector3(inputX, inputY, 0f) * _moveSpeed * Time.deltaTime;

        Vector3 newLocalPos = transform.localPosition + movement;

        newLocalPos.x = Mathf.Clamp(newLocalPos.x, _minBounds.x, _maxBounds.x);
        newLocalPos.y = Mathf.Clamp(newLocalPos.y, _minBounds.y, _maxBounds.y);

        // LERPed Z-plane
        newLocalPos.z = _currentZPlane;

        transform.localPosition = newLocalPos;

        // Tilt rotation
        float tiltZ = -inputX * _tiltAmount;
        float tiltX = inputY * _tiltAmount * _noseTiltY;
        float yawY = inputX * (_tiltAmount * _noseTiltX);

        _targetRotation = Quaternion.Euler(tiltX, yawY, tiltZ);
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            _targetRotation,
            Time.deltaTime * _tiltSmoothSpeed
        );
    }

    // ----------------------------------------------------------
    // BOOST (unchanged)
    // ----------------------------------------------------------
    private void HandleSpeed()
    {
        if (_cooldownTimer > 0)
            _cooldownTimer -= Time.deltaTime;

        if (!_isBoosting && _cooldownTimer <= 0 && _boostButtonPressed)
        {
            _boostButtonPressed = false; // one-shot trigger

            _isBoosting = true;
            _speedVignette.ToggleSpeedVignette(true);
            _vfx.SetEvent("Boost");
            _boostTimer = _boostDuration;

            _zLerpTimer = 0f;
        }

        if (_isBoosting)
        {
            _boostTimer -= Time.deltaTime;

            float t = Mathf.Clamp01(1f - (_boostTimer / _boostDuration));
            float currentSpeed = Mathf.Lerp(_boostSpeed, _travelSpeed, t);

            _cinemachineDollyCart.m_Speed = currentSpeed;

            _zLerpTimer += Time.deltaTime;
            float Zlerp = Mathf.Clamp01(_zLerpTimer / _ZPlaneSmoothness);
            _currentZPlane = Mathf.Lerp(_fixedZPlane, _boostZPlane, Zlerp);

            if (_boostTimer <= 0)
            {
                _isBoosting = false;
                _speedVignette.ToggleSpeedVignette(false);
                _cooldownTimer = _boostCoolDown;
                _zLerpTimer = 0f;
            }
        }
        else
        {
            _cinemachineDollyCart.m_Speed = _travelSpeed;

            if (_currentZPlane != _fixedZPlane)
            {
                _zLerpTimer += Time.deltaTime;
                float z01 = Mathf.Clamp01(_zLerpTimer / 0.35f);
                _currentZPlane = Mathf.Lerp(_boostZPlane, _fixedZPlane, z01);
            }
        }
    }

    // Called by UI button
    public void PressBoost() => _boostButtonPressed = true;
    public void ReleaseBoost() => _boostButtonPressed = false;

    // Boundaries (unchanged)
    private void CalculateMovementBounds()
    {
        if (_mainCamera == null)
            return;

        float distance = Vector3.Dot(transform.parent.position - _mainCamera.transform.position, _mainCamera.transform.forward);

        Vector3 bottomLeft = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 bottomRight = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topLeft = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 topRight = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));

        Transform parent = transform.parent;
        Vector3 localBL = parent.InverseTransformPoint(bottomLeft);
        Vector3 localBR = parent.InverseTransformPoint(bottomRight);
        Vector3 localTL = parent.InverseTransformPoint(topLeft);
        Vector3 localTR = parent.InverseTransformPoint(topRight);

        _minBounds = new Vector2(
            Mathf.Min(localBL.x, localBR.x, localTL.x, localTR.x),
            Mathf.Min(localBL.y, localBR.y, localTL.y, localTR.y)
        );

        _maxBounds = new Vector2(
            Mathf.Max(localBL.x, localBR.x, localTL.x, localTR.x),
            Mathf.Max(localBL.y, localBR.y, localTL.y, localTR.y)
        );

        _minBounds.x -= extraHorizontal;
        _maxBounds.x += extraHorizontal;

        _minBounds.y -= extraVertical;
        _maxBounds.y += extraVertical;
    }
}
