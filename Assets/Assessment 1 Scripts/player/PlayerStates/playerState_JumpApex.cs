using UnityEngine;

public class playerState_JumpApex : player_StateBase
{
    public playerState_JumpApex(playerCharacter playerCharacter, playerStateManager StateManager,
        PlayerCoroutineHandler coroutineHandler) : base(playerCharacter, StateManager, coroutineHandler) { }

    public override void Enter()
    {
        base.Enter();

        CH.RunCoroutine(CH.VerticalDirectionCheck(), CH.C_VerticalDirectionCheck);
    }
    //apex speed
    // apex grava
}
