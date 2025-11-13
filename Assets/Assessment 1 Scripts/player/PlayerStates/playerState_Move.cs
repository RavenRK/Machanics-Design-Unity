using UnityEngine;

public class playerState_Move : player_StateBase
{
    public playerState_Move(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler, PlayerFeedBackManager SoundManager ) : base(playerCharacter, StateManager, coroutineHandler, SoundManager) { }
    public override void Enter() 
    { 
        CH.RunCoroutine(CH.VerticalDirectionCheck(), CH.C_VerticalDirectionCheck); //we falling check
        CH.RunCoroutine(CH.MoveUpdate(), CH.C_MoveCheck); // start moving
        //CH.RunCoroutine(CH.MoveSoundTimer(), CH.C_MoveSoundCheck); // start move sound
    }

    public override void OnJumpPressed()
    {
        base.OnJumpPressed();
        StateManager.ChangeState(StateManager.JumpState);
    }

    #region Empty
    public override void Exit()
    {
        base.Exit();
    }
    public override void OnMove(Vector2 direction)
    {
        base.OnMove(direction);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    #endregion
}
