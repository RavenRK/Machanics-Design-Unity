using UnityEngine;

public class playerState_JumpApex : player_StateBase
{
    public playerState_JumpApex(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler, PlayerSoundManager SoundManager) : base(playerCharacter, StateManager, coroutineHandler, SoundManager) { }

    public override void Enter()
    {
        base.Enter();

        CH.RunCoroutine(CH.VerticalDirectionCheck(), CH.C_VerticalDirectionCheck);

        if (PC.bJumpGravityReset)
        {
            PC.rb2D.gravityScale = PC.apexGravityScale;
            PC.rb2D.AddForce(PC.apexSpeedBoost * PC.Movedirection, ForceMode2D.Impulse);
        }
    }
    public override void Exit()
    {
        base.Exit();
        PC.rb2D.gravityScale = PC.jumpGravity;
    }
}
