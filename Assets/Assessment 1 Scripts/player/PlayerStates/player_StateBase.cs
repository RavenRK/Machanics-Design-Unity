using UnityEditorInternal;
using UnityEngine;

public class player_StateBase
{
    public PlayerInputHandler inputHandler;
    protected playerStateManager StateManager;
    protected PlayerCoroutineHandler CH;
    protected playerCharacter PC;
    protected PlayerFeedBackManager FeedbackM;

    protected player_StateBase(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler, PlayerFeedBackManager FeedbackManager)
    {
        this.StateManager = StateManager;
        this.PC = playerCharacter;
        CH = coroutineHandler;
        FeedbackM = FeedbackManager;
    }
    public virtual void Enter() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { CH.StopAllCoroutines(); }
    public virtual void OnMove(Vector2 direction) 
    {
        PC.Movedirection = direction;
        CH.RunCoroutine(CH.AirMoveUpdate(), CH.C_AirMoveCheck);
    }
    public virtual void OnJumpPressed() { }
    public virtual void OnJumpReleased()
    {
        if (PC.bJumpGravityReset && !PC.bIsGrounded)
            Jump(PC.originalGravityScale, PC.jumpDownForce, false);
    }

    public virtual void OnInteract() 
    {
        //Log.Red("Interact Pressed");
        PC.InteractionCollider = Physics2D.OverlapCircle(PC.transform.position, 1, PC.interactableLayer);
            
        if (PC.InteractionCollider == null)
        {
            //Log.Red("No Interactable Object in Range");
            return;
        }
        if (PC.InteractionCollider != null && PC.InteractionCollider.transform.TryGetComponent<IInteractable>(out var interactableObject))
        {
            interactableObject.Interact();
        }
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
