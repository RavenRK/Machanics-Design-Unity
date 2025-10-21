using UnityEngine;

public class playerState_Move : player_StateBase
{
    public playerState_Move(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }
    public override void Enter() {  }
    public override void Exit() { }
    public override void OnMove(Vector2 direction)
    {
        base.OnMove(direction);
    }

    public override void OnJumpPressed()
    {
        base.OnJumpPressed();
        StateManager.ChangeState(StateManager.JumpState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!PC.bIsGrounded)
        {
            StateManager.ChangeState(StateManager.AirState);
        }
        if (PC.Movedirection != Vector2.zero)
        {
            PC.rb2D.linearVelocity = new Vector2(PC.Movedirection.x * PC.moveSpeed, PC.rb2D.linearVelocity.y);
        }
        else
        {
            StateManager.ChangeState(StateManager.IdleState);
        }
    }
}
