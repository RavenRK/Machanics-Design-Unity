using UnityEngine;

public class playerState_Air : player_StateBase
{
    public playerState_Air(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }

    public override void Enter()
    {
        if (!PC.bIsInputbuffer) // ground check
        CH.RunCoroutine(CH.GroundCheckUpdate(), CH.C_GroundCheck);

        PC.bIsInputbuffer = false;

        if (StateManager.PreviousState == StateManager.MoveState) // Start Coyote time
            CH.RunCoroutine(CH.CoyoteTimeUpdate(), CH.C_CoyoteTimeCheck);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnJumpPressed()
    {
        if (StateManager.PreviousState == StateManager.JumpState)
            CH.RunCoroutine(CH.InputBufferUpdate(), CH.C_InputBufferCheck);
        else if (PC.bCanCoyoteJump == true)
            StateManager.ChangeState(StateManager.JumpState);

    }
    public override void OnJumpReleased()
    {
        if (PC.bIsInputbuffer)
        {
            PC.bShortJump = true;
            if (StateManager.BDebug_State_Air) Log.Yellow("set short jump");
        }
        if (PC.bJumpGravityReset)
        {
            Jump(PC.originalGravityScale, PC.jumpDownForce, false);
        }
    }
    public void Jump(float newjumpGravity, float newjumpForce, bool allowGravityReset)
    {
        PC.rb2D.AddForce(Vector2.up * newjumpForce, ForceMode2D.Impulse);
        PC.rb2D.gravityScale = newjumpGravity;
        PC.bJumpGravityReset = allowGravityReset;
    }
    #region Empty
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
}
