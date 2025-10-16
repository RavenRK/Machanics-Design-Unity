using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class player_StateBase
{
    protected bool BCanDebug = false;
    protected playerStateManager PlayerStateManager;
    protected playerCharacter PC;

    protected Rigidbody2D Rb2D;

    protected player_StateBase(playerCharacter playerCharacter, playerStateManager playerStateManager)
    {
        this.PlayerStateManager = playerStateManager;
        this.PC = playerCharacter;

    }
    public virtual void Enter() => Rb2D = PC.rb2D;
    public virtual void Update() { }
    public virtual void Exit() { }
}
