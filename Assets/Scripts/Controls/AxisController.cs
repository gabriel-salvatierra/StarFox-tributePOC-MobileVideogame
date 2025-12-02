using UnityEngine;
using UnityEngine.EventSystems;

public class AxisController : Controller, IDragHandler, IEndDragHandler
{

    [SerializeField] private float _maxDistance = 15f;
    private Vector3 _initialPos;
    private Vector3 _dir;

    private void Start()
    {
        _initialPos = transform.position;
    }

    public override Vector3 GetMovementInput()
    {
        // Remap Y on Z
        _moveDir = new Vector3(_dir.x, 0f, _dir.y);
        return _moveDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _dir = (Vector3)eventData.position - _initialPos;
        transform.position = _initialPos + Vector3.ClampMagnitude(_dir, _maxDistance);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _initialPos;
        _dir = Vector3.zero;
    }

}
