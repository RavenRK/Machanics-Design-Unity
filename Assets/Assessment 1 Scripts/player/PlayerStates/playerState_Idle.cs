using UnityEngine;

public class playerState_Idle : player_StateBase
{
    public playerState_Idle(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }


    public override void Enter()
    {
        if (PC.Movedirection != Vector2.zero)
            StateManager.ChangeState(StateManager.MoveState);
    }

    public override void OnJumpPressed()
    {
        base.OnJumpPressed();
        StateManager.ChangeState(StateManager.JumpState);
    }
    public override void OnMove(Vector2 direction)
    {
        base.OnMove(direction);
        StateManager.ChangeState(StateManager.MoveState);
    }

}
