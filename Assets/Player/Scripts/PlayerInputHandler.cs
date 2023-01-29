using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControl _inputActions;
    
    [SerializeField] private Movement _movement;

    public static PlayerControl InputActions { get; private set; }

    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }
    private void Awake()
    {
        _inputActions = new PlayerControl();
        InputActions = _inputActions;

        _movement = GetComponent<Movement>();
    }

    private void FixedUpdate()
    {
        var direction = _inputActions.WASD.Move.ReadValue<Vector2>();
        _movement.Move(direction);
    }
}
