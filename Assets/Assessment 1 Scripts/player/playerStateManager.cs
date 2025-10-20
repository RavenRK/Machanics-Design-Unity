using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class playerStateManager : MonoBehaviour
{
    public player_StateBase CurrentState { get; private set; }
    public playerCharacter playerCharacter { get; private set; }
    public PlayerInputHandler inputHandler  { get; private set; }

    public playerState_Idle IdleState { get; private set; }
    public playerState_Air AirState { get; private set; }
    public playerState_Move MoveState { get; private set; }
    public playerState_Jump JumpState { get; private set; }

    private System.Action<Vector2> moveCallback;
    private System.Action jumpPressedCallback;
    private System.Action jumpReleasedCallback;

    private void Awake()
    {
        playerCharacter = GetComponent<playerCharacter>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }
    private void OnEnable()
    {
        inputHandler.OnMove += direction => CurrentState?.OnMove(direction);
        inputHandler.OnJumpPressed += () => CurrentState?.OnJumpPressed();
        inputHandler.OnJumpReleased += () => CurrentState?.OnJumpReleased();


        inputHandler.OnMove += moveCallback;
        inputHandler.OnJumpPressed += jumpPressedCallback;
        inputHandler.OnJumpReleased += jumpReleasedCallback;
    }
    private void Start()
    {
        IdleState = new playerState_Idle(playerCharacter, this);
        AirState = new playerState_Air(playerCharacter, this);
        MoveState = new playerState_Move(playerCharacter, this);
        JumpState = new playerState_Jump(playerCharacter, this);

        Initialize(IdleState);
    }
    private void Update()
    {

        Log.Blue("Current State: " + CurrentState.GetType().Name);
    }
    private void FixedUpdate()
    {
        CurrentState.FixedUpdate();
    }
    private void OnDisable()
    {
        inputHandler.OnMove -= direction => CurrentState?.OnMove(direction);
        inputHandler.OnJumpPressed -= () => CurrentState?.OnJumpPressed();
        inputHandler.OnJumpReleased -= () => CurrentState?.OnJumpReleased();
    }

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
