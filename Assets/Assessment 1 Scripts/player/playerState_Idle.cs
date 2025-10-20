using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class playerState_Idle : player_StateBase
{
    public playerState_Idle(playerCharacter playerCharacter, playerStateManager StateManager) : base(playerCharacter, StateManager) { }



    public override void Enter()
    {
        //PC.rb2D.linearVelocity = new Vector2(0, 0);

    }
    public override void OnJumpPressed()
    {
        base.OnJumpPressed();
        Log.Purple("Idle > Jump ");
        StateManager.ChangeState(StateManager.JumpState);
    }

    public override void OnMove(Vector2 direction)
    {
        base.OnMove(direction);
        Log.Purple("idle > move");
        StateManager.ChangeState(StateManager.MoveState);
    }

}
