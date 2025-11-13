using UnityEngine;

public class playerState_Air : player_StateBase
{
    public playerState_Air(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler, PlayerFeedBackManager SoundManager) : base(playerCharacter, StateManager, coroutineHandler, SoundManager) { }

    public override void Enter()
    {
        if (!PC.bIsInputbuffer) // ground check
            CH.RunCoroutine(CH.GroundCheckUpdate(), CH.C_GroundCheck);

        PC.bIsInputbuffer = false;

        if (StateManager.PreviousState == StateManager.MoveState) // Start Coyote time
            CH.RunCoroutine(CH.CoyoteTimeUpdate(), CH.C_CoyoteTimeCheck);

        PC.rb2D.linearDamping = PC.jumpLinearDamping; // set air damping
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnJumpPressed()
    {
        base.OnJumpPressed();

        if (StateManager.PreviousState == StateManager.JumpApex)
            CH.RunCoroutine(CH.InputBufferUpdate(), CH.C_InputBufferCheck);
        else if (PC.bCanCoyoteJump == true)
            StateManager.ChangeState(StateManager.JumpState);

    }
    public override void OnJumpReleased()
    {
        base.OnJumpReleased();
        if (PC.bIsInputbuffer)
        {
            PC.bShortJump = true;
            if (StateManager.BDebug_State_Air) Log.Yellow("set short jump");
        }
    }
    public override void Exit()
    {
        base.Exit();
        PC.rb2D.linearDamping = PC.originalLinearDamping;
    }
}
