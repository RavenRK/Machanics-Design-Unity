using UnityEngine;

public class playerState_Air : player_StateBase
{
    public playerState_Air(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }

    public override void Enter()
    {
        PC.bIsInputbuffer = false;
        CH.RunCoroutine(CH.GroundCheckUpdate(), CH.C_GroundCheck);
        if (StateManager.PreviousState == StateManager.MoveState)
        {
            CH.RunCoroutine(CH.CoyoteTimeUpdate(), CH.C_CoyoteTimeCheck);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnJumpPressed()
    {
        if (StateManager.PreviousState == StateManager.JumpState)
        {
            CH.RunCoroutine(CH.InputBufferUpdate(), CH.C_InputBufferCheck);
            PC.bIsInputbuffer = true;
        }
        else if (PC.bCanCoyoteJump == true)
        {
            StateManager.ChangeState(StateManager.JumpState);
        }


    }
    public override void OnJumpReleased()
    {
        if (PC.bIsInputbuffer)
        {
            PC.bShortJump = true;
            Log.Yellow("set short jump");
        }
        if (PC.bJumpGravityReset)
        {
            Jump(PC.originalGravityScale, PC.jumpDownForce, false);
        }
        StateManager.ChangeState(StateManager.AirState);
    }
    public void Jump(float newjumpGravity, float newjumpForce, bool allowGravityReset)
    {
        PC.rb2D.AddForce(Vector2.up * newjumpForce, ForceMode2D.Impulse);
        PC.rb2D.gravityScale = newjumpGravity;
        PC.bJumpGravityReset = allowGravityReset;
    }
}
