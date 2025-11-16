using UnityEngine;

public class playerState_JumpApex : player_StateBase
{
    public playerState_JumpApex(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler, PlayerFeedBackManager SoundManager) : base(playerCharacter, StateManager, coroutineHandler, SoundManager) { }

    public override void Enter()
    {
        base.Enter();

        CH.RunCoroutine(CH.VerticalDirectionCheck(), CH.C_VerticalDirectionCheck);
        CH.RunCoroutine(CH.ColliderPinchCheck(), CH.C_ColliderPinchCheck);

        if (PC.bJumpGravityReset)
        {
            PC.rb2D.gravityScale = PC.apexGravityScale;
            PC.rb2D.AddForce(PC.apexSpeedBoost * PC.Movedirection, ForceMode2D.Impulse);
        }

        PC.playerCapsuleCollider.transform.Rotate(0, 0, 90);
        PC.playerCapsuleCollider.size = PC.JumpColliderSize;
    }
    public override void Exit()
    {
        base.Exit();
        PC.rb2D.gravityScale = PC.jumpGravity;
        PC.playerCapsuleCollider.transform.Rotate(0, 0, -90);
        PC.playerCapsuleCollider.size = PC.originalColliderSize;
    }
}
