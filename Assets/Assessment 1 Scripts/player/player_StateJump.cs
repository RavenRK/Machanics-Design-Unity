using UnityEngine;
using UnityEngine.InputSystem;
public class player_StateJump : player_StateBase
{
    public player_StateJump(playerCharacter _playerCharacter, playerStateManager _playerStateManager) : base(_playerCharacter, _playerStateManager) { }

    public override void Enter()
    {
        base.Enter();

    }

    #region --JUMP---
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (pC.bIsGrounded && !pC.bIsInputbuffer)
                Jump(pC.JumpGravity, pC.jumpForce, true);
            else
                pC.inputBufferUpdate();
        }

        if (context.canceled && pC.bJumpGravityReset)              //buttom up
        {
            if (bCanDebug) { Log.Green("Fall"); }
            Jump(pC.originalGravityScale, pC.JumpDownForce, false);   //reduce gravity and add down force
        }
        else if (context.canceled && pC.bIsInputbuffer)
        {
            pC.bShortJump = true;
        }

    }
    public void Jump(float NewGracityScale, float NewJumpPower, bool allowGracityReset)
    {
        rb2D.AddForce(Vector2.up * NewJumpPower, ForceMode2D.Impulse);
        rb2D.gravityScale = NewGracityScale;
        pC.bJumpGravityReset = allowGracityReset;                          //should we let the player recast Down force at end Jump

        rb2D.linearDamping = pC.JumpLinearDamping;
        pC.CGroundUpdate = pC.StartCoroutine(pC.GroundCheckUpdate());
    }


    #endregion
}
