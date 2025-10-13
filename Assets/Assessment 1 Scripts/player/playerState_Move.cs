using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerState_Move : player_StateBase
{
    public playerState_Move(playerCharacter playerCharacter, playerStateManager playerStateManager) : base(playerCharacter, playerStateManager) { }


    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (bCanDebug) { Log.Green("move"); }

            pC.bIsMoving = true;

            pC.movementInput = context.ReadValue<float>();
            pC.Cmove = pC.StartCoroutine(pC.MoveUpdate());
        }

        if (context.canceled)
        {
            if (bCanDebug) { Log.yellow("stop"); }

            pC.bIsMoving = false;

            pC.movementInput = 0;

            if (pC.bIsGrounded)
                rb2D.linearVelocity = new Vector2(0, 0);
        }
    }
}
