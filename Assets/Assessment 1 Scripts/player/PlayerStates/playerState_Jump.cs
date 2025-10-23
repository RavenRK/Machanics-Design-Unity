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
                PC.bShortJump = false;
                Log.Red("Short Jump Activated");
                Jump(PC.originalGravityScale, PC.jumpForce, true);
                PC.bCanJump = false;
            }
            else
            {
                Jump(PC.jumpGravity, PC.jumpForce, true);
                PC.bCanJump = false;
            }
            CH.RunCoroutine(CH.VerticalDirectionCheck(), CH.C_VerticalDirectionCheck);
        }

    }

    public override void Exit()
    {
        base.Exit();

    }

    #region --JUMP---
    public override void OnJumpReleased()
    {
        base.OnJumpReleased();
        if (PC.bJumpGravityReset)
        {
            Jump(PC.originalGravityScale, PC.jumpDownForce, false);
            //PC.originalValuesReset();
            StateManager.ChangeState(StateManager.AirState);
        }
    }

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
