using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerState_Move : player_StateBase
{
    public playerState_Move(playerCharacter playerCharacter, playerStateManager StateManager) : base(playerCharacter, StateManager) { }

    private bool BcanMove = false;
    Vector2 Movedirection;
    public override void OnMove(Vector2 direction)
    {
        base.OnMove(direction);
        Log.Purple("in move doing move");
        BcanMove = true;
        Movedirection = direction;
    }

    public override void OnJumpPressed()
    {
        base.OnJumpPressed();
        Log.Purple("Move > Jump ");
        StateManager.ChangeState(StateManager.JumpState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!PC.bIsGrounded)
        {
            StateManager.ChangeState(StateManager.AirState);
        }
        if (BcanMove)
        {
            PC.rb2D.linearVelocity = new Vector2(Movedirection.x * PC.moveSpeed, PC.rb2D.linearVelocity.y);
        }
    }
}
