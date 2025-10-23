using UnityEngine;

public class playerState_Idle : player_StateBase
{
    public playerState_Idle(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }

    public override void Enter()
    {
        PC.bCanJump = true;

        if (PC.Movedirection != Vector2.zero)// >> ye input
            StateManager.ChangeState(StateManager.MoveState);// >> start move

        if (PC.Movedirection == Vector2.zero)// >> no input
            PC.rb2D.linearVelocity *= PC.ApplyLandingDamping;// >> fix sliding

        CH.RunCoroutine(CH.VerticalDirectionCheck(), CH.C_VerticalDirectionCheck);

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
    #region Empty
    public override void Exit()
    {
        base.Exit();
    }

    #endregion
}
