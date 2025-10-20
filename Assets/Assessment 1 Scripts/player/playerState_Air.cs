using UnityEngine;

public class playerState_Air : player_StateBase
{
    public playerState_Air(playerCharacter playerCharacter, playerStateManager playerStateManager) : base(playerCharacter, playerStateManager) { }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (PC.bIsGrounded)
        {
            PC.rb2D.linearDamping = PC.originalLinearDamping;
            StateManager.ChangeState(StateManager.IdleState);
        }
    }

    public void Jump(float newjumpGravity, float newjumpForce, bool allowGravityReset)
    {
        PC.rb2D.AddForce(Vector2.up * newjumpForce, ForceMode2D.Impulse);
        PC.rb2D.gravityScale = newjumpGravity;
        PC.bJumpGravityReset = allowGravityReset;                          //should we let the player recast Down force at end Jump

        //PC.CGroundUpdate = PC.StartCoroutine(PC.GroundCheckUpdate());
    }
}
