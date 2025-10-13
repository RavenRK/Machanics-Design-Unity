using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class player_StateBase
{
    protected bool bCanDebug = false;
    protected playerStateManager playerStateManager;
    protected playerCharacter pC;

    protected Rigidbody2D rb2D;

    public player_StateBase(playerCharacter _playerCharacter, playerStateManager _playerStateManager)
    {
        this.playerStateManager = _playerStateManager;
        this.pC = _playerCharacter;

    }
    public virtual void Enter() => rb2D = pC.rb2D;
    public virtual void Update() { }
    public virtual void Exit() { }
}
