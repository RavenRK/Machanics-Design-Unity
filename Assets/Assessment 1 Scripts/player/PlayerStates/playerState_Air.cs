using UnityEngine;

public class playerState_Air : player_StateBase
{
    public playerState_Air(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }

    bool bShortJump = false;
    public bool bIsInputbuffer = false;

    public override void Enter()
    {
        //CH.StartCoroutine(CH.GroundCheckUpdate());
        bShortJump = false;
        bIsInputbuffer = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (PC.bIsGrounded)
        {
            PC.rb2D.linearDamping = PC.originalLinearDamping;
            StateManager.ChangeState(StateManager.IdleState);
        }
    }
    public override void OnJumpPressed()
    {
        //InputBuffer(bShortJump);
    }
    public override void OnJumpReleased()
    {
        if (bIsInputbuffer)
            bShortJump = true;
    }
    public void Jump(float newjumpGravity, float newjumpForce, bool allowGravityReset)
    {
        PC.rb2D.AddForce(Vector2.up * newjumpForce, ForceMode2D.Impulse);
        PC.rb2D.gravityScale = newjumpGravity;
        PC.bJumpGravityReset = allowGravityReset;                          //should we let the player recast Down force at end Jump

        //PC.CGroundUpdate = PC.StartCoroutine(PC.GroundCheckUpdate());
    }
    public void InputBuffer(bool bShortJump)
    {
        bIsInputbuffer = true;
        // do normal jump if buffer timer is > 0 when player is grounded
        if (PC.jumpBufferTimer > 0 && PC.bIsGrounded && !bShortJump)
        {
            //if (bCanDebug) { Log.Green("input buffer Jump"); }
            StateManager.ChangeState(StateManager.JumpState);
        }
        else if (PC.jumpBufferTimer > 0 && PC.bIsGrounded && bShortJump)
        {
            //if (bCanDebug) { Log.Yellow("input buffer Short Jump"); }
            StateManager.ChangeState(StateManager.JumpState);
            Log.Yellow("Short Jump executed and set back to false");
        }
        else if (PC.jumpBufferTimer < 0)
        {
            bIsInputbuffer = false;
            bShortJump = false;
            //if (bCanDebug) { Log.Yellow("Input buffer time out"); }
        }
    }
}
