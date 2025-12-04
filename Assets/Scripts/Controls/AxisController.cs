using UnityEngine;
using UnityEngine.EventSystems;

public class AxisController : Controller, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _maxDistance = 80f; // UI units
    private RectTransform _rectTransform;
    private Vector2 _startPos;   // UI-local position
    private Vector2 _dragDelta;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _startPos = _rectTransform.anchoredPosition;
    }

    public override Vector3 GetMovementInput()
    {
        // UI uses x (horizontal) and y (vertical UI) > remap y to Z axis
        _moveDir = new Vector3(_dragDelta.x, 0f, _dragDelta.y).normalized;
        return _moveDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert drag movement into local UI delta
        Vector2 localMouse;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localMouse
        );

        _dragDelta = localMouse - _startPos;

        // Limit inside circle radius
        _dragDelta = Vector2.ClampMagnitude(_dragDelta, _maxDistance);

        // Move UI joystick handle
        _rectTransform.anchoredPosition = _startPos + _dragDelta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _startPos;
        _dragDelta = Vector2.zero;
    }
}
