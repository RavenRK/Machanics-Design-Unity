using UnityEngine;
using UnityEngine.InputSystem;
public class playerState_Jump : player_StateBase
{
    public playerState_Jump(playerCharacter playerCharacter, playerStateManager playerStateManager) : base(playerCharacter, playerStateManager) { }

    public override void Enter()
    {
        base.Enter();
        
        if (PC.bIsGrounded && PC.bJumpGravityReset)
            Jump(PC.originalGravityScale, PC.jumpForce, true);
        else if (PC.bShortJump)
            Jump(PC.jumpGravity, PC.jumpForce, true);
    }

    #region --JUMP---
    public void Jump(InputAction.CallbackContext context)
    {

        if (context.canceled && PC.bJumpGravityReset)              //bottom up
        {
            Jump(PC.originalGravityScale, PC.jumpDownForce, false);   //reduce gravity and add down force
        }
        else if (context.canceled && PC.bIsInputbuffer)
        {
            PC.bShortJump = true;
        }

    }
    public void Jump(float newGravityScale, float newJumpPower, bool allowGravityReset)
    {
        Rb2D.AddForce(Vector2.up * newJumpPower, ForceMode2D.Impulse);
        Rb2D.gravityScale = newGravityScale;
        PC.bJumpGravityReset = allowGravityReset;                          //should we let the player recast Down force at end Jump

        Rb2D.linearDamping = PC.jumpLinearDamping;
        PC.CGroundUpdate = PC.StartCoroutine(PC.GroundCheckUpdate());
    }


    #endregion
}
