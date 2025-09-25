using UnityEngine;

public class ButtonsController : Controller
{
    public void MoveUp() { _moveDir = Vector3.forward; }
    public void MoveDown() { _moveDir = Vector3.back; }
    public void MoveLeft() { _moveDir = Vector3.left; }
    public void MoveRight() { _moveDir = Vector3.right; }
    public void StopMovement() { _moveDir = Vector3.zero; }



}
