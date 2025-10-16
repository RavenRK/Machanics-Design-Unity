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
            if (BCanDebug) { Log.Green("move"); }

            PC.bIsMoving = true;

            PC.movementInput = context.ReadValue<float>();
            PC.Cmove = PC.StartCoroutine(PC.MoveUpdate());
        }

        if (context.canceled)
        {
            if (BCanDebug) { Log.Yellow("stop"); }

            PC.bIsMoving = false;

            PC.movementInput = 0;

            if (PC.bIsGrounded)
                Rb2D.linearVelocity = new Vector2(0, 0);
        }
    }
}
