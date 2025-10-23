using UnityEngine;
using System.Collections;

public class playerStateManager : MonoBehaviour
{
    public bool BDebug_CurrentState = false;
    public bool BDebug_StateChange = false;

    public player_StateBase CurrentState { get; private set; }
    public player_StateBase PreviousState { get; private set; }
    public playerCharacter playerCharacter { get; private set; }
    public PlayerInputHandler inputHandler  { get; private set; }
    public PlayerCoroutineHandler coroutineHandler { get; private set; }

    #region Define States
    public playerState_Idle IdleState { get; private set; }
    public playerState_Air AirState { get; private set; }
    public playerState_Move MoveState { get; private set; }
    public playerState_Jump JumpState { get; private set; }
    #endregion

    private System.Action<Vector2> moveCallback;
    private System.Action jumpPressedCallback;
    private System.Action jumpReleasedCallback;

    private void Awake()
    {
        playerCharacter = GetComponent<playerCharacter>();
        inputHandler = GetComponent<PlayerInputHandler>();
        coroutineHandler = GetComponent<PlayerCoroutineHandler>();
    }
    
    private void Start()
    {
        IdleState = new playerState_Idle(playerCharacter, this, coroutineHandler);
        AirState = new playerState_Air(playerCharacter, this, coroutineHandler);
        MoveState = new playerState_Move(playerCharacter, this, coroutineHandler);
        JumpState = new playerState_Jump(playerCharacter, this, coroutineHandler);

        Initialize(IdleState);
    }
    private void Update()
    {
        if (BDebug_CurrentState) 
            Log.Blue("Current State: " + CurrentState.GetType().Name);
    }
    private void FixedUpdate()
    {
        CurrentState.FixedUpdate();
    }

    public Coroutine RunCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StopPlayerCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    #region player input event subscriptions
    private void OnEnable()
    {
        inputHandler.OnMove += direction => CurrentState?.OnMove(direction);
        inputHandler.OnJumpPressed += () => CurrentState?.OnJumpPressed();
        inputHandler.OnJumpReleased += () => CurrentState?.OnJumpReleased();


        inputHandler.OnMove += moveCallback;
        inputHandler.OnJumpPressed += jumpPressedCallback;
        inputHandler.OnJumpReleased += jumpReleasedCallback;
    }
    private void OnDisable()
    {
        inputHandler.OnMove -= direction => CurrentState?.OnMove(direction);
        inputHandler.OnJumpPressed -= () => CurrentState?.OnJumpPressed();
        inputHandler.OnJumpReleased -= () => CurrentState?.OnJumpReleased();
    }
    #endregion
    #region initialize and ChangState
    public void Initialize(player_StateBase startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }
    public void ChangeState(player_StateBase newState)
    {
        if (CurrentState != newState)
        {
            if (BDebug_StateChange) Log.Green(CurrentState.ToString() + " > " + newState.ToString());
            PreviousState = CurrentState; //set previous state to current state
            CurrentState.Exit();        //call exit on the current state
            CurrentState = newState;   //set the current state to the new state
            CurrentState.Enter();       //call enter on the new state
        }
        else
            if (BDebug_StateChange) Log.Yellow("Already in " + newState.ToString());
    }
    #endregion
}
