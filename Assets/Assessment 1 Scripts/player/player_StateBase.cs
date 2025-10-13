using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class player_StateBase
{
    protected bool bCanDebug = false;
    protected playerStateManager playerStateManager;
    protected playerCharacter pC;

    protected Rigidbody2D rb2D;

    protected player_StateBase(playerCharacter playerCharacter, playerStateManager playerStateManager)
    {
        this.playerStateManager = playerStateManager;
        this.pC = playerCharacter;

    }
    public virtual void Enter() => rb2D = pC.rb2D;
    public virtual void Update() { }
    public virtual void Exit() { }
}
