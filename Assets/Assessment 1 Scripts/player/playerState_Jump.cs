using UnityEngine;
using UnityEngine.InputSystem;
public class playerState_Jump : player_StateBase
{
    public playerState_Jump(playerCharacter playerCharacter, playerStateManager playerStateManager) : base(playerCharacter, playerStateManager) { }

    public override void Enter()
    {
        base.Enter();
        
        if (pC.bIsGrounded && pC.bJumpGravityReset)
            Jump(pC.originalGravityScale, pC.JumpForce, true);
        else if (pC.bShortJump)
            Jump(pC.JumpGravity, pC.JumpForce, true);
    }

    #region --JUMP---
    public void Jump(InputAction.CallbackContext context)
    {

        if (context.canceled && pC.bJumpGravityReset)              //buttom up
        {
            Jump(pC.originalGravityScale, pC.JumpDownForce, false);   //reduce gravity and add down force
        }
        else if (context.canceled && pC.bIsInputbuffer)
        {
            pC.bShortJump = true;
        }

    }
    public void Jump(float newGravityScale, float newJumpPower, bool allowGravityReset)
    {
        rb2D.AddForce(Vector2.up * newJumpPower, ForceMode2D.Impulse);
        rb2D.gravityScale = newGravityScale;
        pC.bJumpGravityReset = allowGravityReset;                          //should we let the player recast Down force at end Jump

        rb2D.linearDamping = pC.JumpLinearDamping;
        pC.CGroundUpdate = pC.StartCoroutine(pC.GroundCheckUpdate());
    }


    #endregion
}
