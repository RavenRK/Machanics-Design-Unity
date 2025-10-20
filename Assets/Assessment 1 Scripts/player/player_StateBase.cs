using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class player_StateBase
{

    public PlayerInputHandler inputHandler;

    protected playerStateManager StateManager;
    protected playerCharacter PC;

    private void Start()
    {
        // Subscribe the current state to input events
        inputHandler.OnMove += dir => StateManager.CurrentState?.OnMove(dir);
        inputHandler.OnJumpPressed += () => StateManager.CurrentState?.OnJumpPressed();
        inputHandler.OnJumpReleased += () => StateManager.CurrentState?.OnJumpReleased();
    }

    protected player_StateBase(playerCharacter playerCharacter, playerStateManager StateManager)
    {
        this.StateManager = StateManager;
        this.PC = playerCharacter;

    }
    public virtual void Enter() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }

    public virtual void OnMove(Vector2 direction) { }
    public virtual void OnJumpPressed() { }
    public virtual void OnJumpReleased() {  }
}
