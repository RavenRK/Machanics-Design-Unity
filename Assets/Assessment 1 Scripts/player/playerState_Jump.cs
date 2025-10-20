using UnityEngine;
using UnityEngine.InputSystem;
public class playerState_Jump : player_StateBase
{
    public playerState_Jump(playerCharacter playerCharacter, playerStateManager StateManager) : base(playerCharacter, StateManager) { }

    public override void Enter()
    {
        base.Enter();
        Log.Purple("in jump do jump");
        Jump(PC.jumpGravity, PC.jumpForce, true);
    }

    #region --JUMP---
    public override void OnJumpReleased()
    {
        base.OnJumpReleased();
        Log.Purple("air Jump relesed");
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
        PC.bJumpGravityReset = allowGravityReset;                          //should we let the player recast Down force at end Jump

        PC.rb2D.linearDamping = PC.jumpLinearDamping;
        //PC.CGroundUpdate = PC.StartCoroutine(PC.GroundCheckUpdate());
    }
    #endregion
}
