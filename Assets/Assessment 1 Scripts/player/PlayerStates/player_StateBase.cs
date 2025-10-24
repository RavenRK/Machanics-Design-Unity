using UnityEditorInternal;
using UnityEngine;

public class player_StateBase
{
    public PlayerInputHandler inputHandler;
    protected playerStateManager StateManager;
    protected PlayerCoroutineHandler CH;
    protected playerCharacter PC;

    protected player_StateBase(playerCharacter playerCharacter, playerStateManager StateManager, PlayerCoroutineHandler coroutineHandler)
    {
        this.StateManager = StateManager;
        this.PC = playerCharacter;
        CH = coroutineHandler;
    }
    public virtual void Enter() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() 
    {
        CH.StopAllCoroutines();
    }

    public virtual void OnMove(Vector2 direction) 
    {
        PC.Movedirection = direction;
        if (!PC.bIsGrounded)
        {
            Log.Green("Air Move Applied");
            PC.rb2D.AddForce(new Vector2(PC.Movedirection.x * PC.airMoveSpeed, 0f), ForceMode2D.Force);

        }

    }
    public virtual void OnJumpPressed() { }
    public virtual void OnJumpReleased()
    {
        //if (PC.bShortJump)
        //    Jump(PC.originalGravityScale, PC.ShortJumpForce, false);
        if (PC.bJumpGravityReset)
            Jump(PC.originalGravityScale, PC.jumpDownForce, false);
    }

    public void Jump(float newjumpGravity, float newjumpForce, bool allowGravityReset)
    {

        PC.rb2D.AddForce(Vector2.up * newjumpForce, ForceMode2D.Impulse);
        PC.rb2D.gravityScale = newjumpGravity;
        PC.bJumpGravityReset = allowGravityReset;

        PC.rb2D.linearDamping = PC.jumpLinearDamping;
        CH.RunCoroutine(CH.GroundCheckUpdate(), CH.C_GroundCheck);
    }
}
