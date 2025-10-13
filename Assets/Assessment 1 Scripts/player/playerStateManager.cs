using UnityEngine;
using UnityEngine.Playables;

public class playerStateManager : MonoBehaviour
{
    public player_StateBase currentState { get; private set; }
    public playerCharacter playerCharacter;

    //set current state to the start state and call enter
    public void Initialize(player_StateBase _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
    public void ChangeState(player_StateBase _newState)
    {
        currentState.Exit();        //call exit on the current state
        currentState = _newState;   //set the current state to the new state
        currentState.Enter();       //call enter on the new state
    }
}
