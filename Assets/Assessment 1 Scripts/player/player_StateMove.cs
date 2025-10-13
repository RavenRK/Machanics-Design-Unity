using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_StateMove : player_StateBase
{
    public player_StateMove(playerCharacter _playerCharacter, playerStateManager _playerStateManager) : base(_playerCharacter, _playerStateManager) { }


    public void move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (bCanDebug) { Log.Green("move"); }

            pC.IsMoving = true;

            pC.movementInput = context.ReadValue<float>();
            pC.Cmove = pC.StartCoroutine(pC.MoveUpdate());
        }

        if (context.canceled)
        {
            if (bCanDebug) { Log.yellow("stop"); }

            pC.IsMoving = false;

            pC.movementInput = 0;

            if (pC.bIsGrounded)
                rb2D.linearVelocity = new Vector2(0, 0);
        }
    }
}
