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
    }
    public virtual void OnJumpPressed() { }
    public virtual void OnJumpReleased() {  }
}
