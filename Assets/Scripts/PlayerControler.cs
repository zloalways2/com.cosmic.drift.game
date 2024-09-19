using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float _speed;

    private MovementStates _currentMovementState = MovementStates.Idle;

    private void Update()
    {
        if (_currentMovementState == MovementStates.Right && transform.localPosition.x < 360)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
        else if (_currentMovementState == MovementStates.Left && transform.localPosition.x > -360)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }

    public void MoveLeft()
    {
        _currentMovementState = MovementStates.Left;
    }

    public void MoveRight()
    {
        _currentMovementState = MovementStates.Right;
    }

    public void Idle()
    {
        _currentMovementState = MovementStates.Idle;
    }

    public enum MovementStates
    {
        Idle,
        Right,
        Left
    }
}
