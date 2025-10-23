using UnityEngine;

public class playerState_Jump : player_StateBase
{
    public playerState_Jump(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }
    public override void Enter()
    {
        base.Enter();
        if (PC.bCanJump)
        {
            if (PC.bShortJump)
            {
                if (StateManager.BDebug_State_Jump) Log.Red("Short Jump Activated");
                Jump(PC.originalGravityScale, PC.jumpForce, true);
                PC.bShortJump = false;
                PC.bCanJump = false;
            }
            else if (PC.bCanCoyoteJump)
            {
                if (StateManager.BDebug_State_Jump) Log.Red("Input CoyoteTimeJumPower Activated");
                Jump(PC.jumpGravity, PC.CoyoteTimeJumPower, true);
                PC.bCanJump = false;
                PC.bCanCoyoteJump = false;
            }
            else
            {
                if (StateManager.BDebug_State_Jump) Log.Red("Jump Activated");
                Jump(PC.jumpGravity, PC.jumpForce, true);
                PC.bCanJump = false; 
            }
            CH.RunCoroutine(CH.VerticalDirectionCheck(), CH.C_VerticalDirectionCheck);
        }
        else
        {
            if (StateManager.BDebug_State_Jump) Log.Red("we cant jumnp");
        }
    }

    public override void Exit()
    {
        base.Exit();
        PC.bIsInputbuffer = false;
    }

    public override void OnJumpReleased()
    {
        base.OnJumpReleased();
        if (PC.bJumpGravityReset)
        {
            Jump(PC.originalGravityScale, PC.jumpDownForce, false);
            StateManager.ChangeState(StateManager.AirState);
        }
    }

    #region --JUMP---
    public void Jump(float newjumpGravity, float newjumpForce, bool allowGravityReset)
    {

        PC.rb2D.AddForce(Vector2.up * newjumpForce, ForceMode2D.Impulse);
        PC.rb2D.gravityScale = newjumpGravity;
        PC.bJumpGravityReset = allowGravityReset; 

        PC.rb2D.linearDamping = PC.jumpLinearDamping;
        CH.RunCoroutine(CH.GroundCheckUpdate(),CH.C_GroundCheck);
    }
    #endregion
}
