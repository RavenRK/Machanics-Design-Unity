using UnityEngine;
using UnityEngine.Playables;

public class playerStateManager : MonoBehaviour
{
    public player_StateBase CurrentState { get; private set; }
    public playerCharacter playerCharacter;

    //set current state to the start state and call enter
    public void Initialize(player_StateBase startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }
    public void ChangeState(player_StateBase newState)
    {
        CurrentState.Exit();        //call exit on the current state
        CurrentState = newState;   //set the current state to the new state
        CurrentState.Enter();       //call enter on the new state
    }
}
