using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class playerState_Idle : player_StateBase
{
    public playerState_Idle(playerCharacter playerCharacter, playerStateManager playerStateManager) : base(playerCharacter, playerStateManager) { }
    public override void Enter()
    {
        base.Enter();
        Rb2D.linearVelocity = new Vector2(0, 0);
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
            if (PC.bIsGrounded && !PC.bIsInputbuffer)
                PlayerStateManager.ChangeState(PC.JumpState);
            else
                PC.InputBufferUpdate();
        }

        if (context.canceled && PC.bJumpGravityReset)              //buttom up
        {
            if (BCanDebug) { Log.Green("Fall"); }
            //Jump(pC.originalGravityScale, pC.JumpDownForce, false);   //reduce gravity and add down force
        }
        else if (context.canceled && PC.bIsInputbuffer)
        {
            PC.bShortJump = true;
        }

    }
}
