using UnityEngine;
using UnityEngine.Windows;

public class player_StateIdle : player_StateBase
{
    public player_StateIdle(playerCharacter _playerCharacter, playerStateManager _playerStateManager) : base(_playerCharacter, _playerStateManager) { }
    public override void Enter()
    {
        base.Enter();
        rb2D.linearVelocity = new Vector2(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

    }
}
