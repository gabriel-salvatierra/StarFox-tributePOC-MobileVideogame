using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private float _speed = 5f;

    private void Update()
    {
        transform.position += _controller.GetMovementInput() * (_speed * Time.deltaTime);
    }

}
