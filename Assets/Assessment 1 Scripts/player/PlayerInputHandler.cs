using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action OnJumpPressed;
    public event Action OnJumpReleased;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;


    void Awake() // Initialize input actions
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        jumpAction = playerInput.actions["jump"];
    }
    private void OnEnable() // Subscribe to input events
    {
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        jumpAction.performed += OnJumpPerformed;
        jumpAction.canceled += OnJumpCanceled;
    }

    private void OnDisable() // Unsubscribe from input events
    {
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
        jumpAction.performed -= OnJumpPerformed;
        jumpAction.canceled -= OnJumpCanceled;
    }
    // Input action callbacks
    private void OnMovePerformed(InputAction.CallbackContext context)
        => OnMove?.Invoke(new Vector2(context.ReadValue<float>(), 0));

    private void OnMoveCanceled(InputAction.CallbackContext context)
        => OnMove?.Invoke(Vector2.zero);

    private void OnJumpPerformed(InputAction.CallbackContext context)
        => OnJumpPressed?.Invoke();

    private void OnJumpCanceled(InputAction.CallbackContext context)
        => OnJumpReleased?.Invoke();
}
