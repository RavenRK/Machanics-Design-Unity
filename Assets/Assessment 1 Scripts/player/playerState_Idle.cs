using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class playerState_Idle : player_StateBase
{
    public playerState_Idle(playerCharacter playerCharacter, playerStateManager playerStateManager) : base(playerCharacter, playerStateManager) { }
    public override void Enter()
    {
        base.Enter();
        rb2D.linearVelocity = new Vector2(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (pC.bIsGrounded && !pC.bIsInputbuffer)
                playerStateManager.ChangeState(pC.JumpState);
            else
                pC.InputBufferUpdate();
        }

        if (context.canceled && pC.bJumpGravityReset)              //buttom up
        {
            if (bCanDebug) { Log.Green("Fall"); }
            //Jump(pC.originalGravityScale, pC.JumpDownForce, false);   //reduce gravity and add down force
        }
        else if (context.canceled && pC.bIsInputbuffer)
        {
            pC.bShortJump = true;
        }

    }
}
