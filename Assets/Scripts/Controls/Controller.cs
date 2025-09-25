using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Vector3 _moveDir;

    public virtual Vector3 GetMovementInput()
    {
        return _moveDir;
    }
}
