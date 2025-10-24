using UnityEngine;

public class playerState_Jump : player_StateBase
{
    public playerState_Jump(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }
    public override void Enter()
    {
        base.Enter();
        EnterJumpStateBaseJumpFunc();
    }
    public override void OnJumpReleased()
    {
        base.OnJumpReleased();

    }
    public override void Exit()
    {
        base.Exit();
        PC.bIsInputbuffer = false;
    }

    #region --JUMP---
    public void EnterJumpStateBaseJumpFunc()
    {
        if (PC.bCanJump)
        {
            if (PC.bShortJump)
            {
                if (StateManager.BDebug_State_Jump) Log.Red("Short Jump Activated");
                Jump(PC.originalGravityScale, PC.ShortJumpForce, true);
                PC.bShortJump = false;
                PC.bCanJump = false;
            }
            else if (PC.bCanCoyoteJump)
            {
                if (StateManager.BDebug_State_Jump) Log.Red("Input CoyoteTimeJumPower Activated");
                PC.rb2D.linearVelocity = new Vector2(PC.moveSpeed, 0) * PC.Movedirection;
                Jump(PC.jumpGravity, PC.jumpForce, true);
                PC.bCanJump = false;
                PC.bCanCoyoteJump = false;
            }
            else
            {
                if (StateManager.BDebug_State_Jump) Log.Red("Jump Activated");
                Jump(PC.jumpGravity, PC.jumpForce, true);
                PC.bCanJump = false;
            }
            CH.RunCoroutine(CH.JumpApexUpdate(), CH.C_JumpApexCheck);
        }
        else
        {
            if (StateManager.BDebug_State_Jump) Log.Red("we cant jumnp");
        }
    }

    #endregion
}
