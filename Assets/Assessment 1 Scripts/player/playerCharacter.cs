using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using UnityEngine.Serialization;

#region --Debug---
public static class Log
{
    public static void Red(string msg) => Debug.Log($" <color=red>{msg}</color>");
    public static void Yellow(string msg) => Debug.Log($" <color=yellow>{msg}</color>");
    public static void Green(string msg) => Debug.Log($" <color=lime>{msg}</color>");
    public static void Blue(string msg) => Debug.Log($" <color=cyan>{msg}</color>");
    public static void Purple(string msg) => Debug.LogError($" <color=violet>{msg}</color>");
}
#endregion

public class playerCharacter : MonoBehaviour
{
    int _currentHealth = 100;
    public event Action OnPlayerDead;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            OnPlayerDead?.Invoke();
        }
    }

    public playerStateManager PlayerStateManager { get; private set; }
    private PlayerInput playerInput;

    #region --movement settings---
    #region --JUMP Settings---
    [FormerlySerializedAs("JumpForce")]
    [Header("Jump Settings")]
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public float jumpGravity = 5f;
    [SerializeField] public float jumpDownForce = 5f;
    [SerializeField] public float jumpBufferTimer = 0.2f;
    [SerializeField] public float jumpLinearDamping = 0;
    public float originalJumpBufferTimer;
    public float originalGravityScale;
    public bool bIsInputbuffer = false;
    public bool bShortJump = false;
    public float originalLinearDamping;
    public Coroutine CGroundUpdate; //rhzhaw
    public bool bJumpGravityReset = true;
    #endregion

    #region --Move Settings---
    [Header("Move settings")]
    [SerializeField] private float moveSpeed = 5f;
    public float movementInput;
    public Coroutine Cmove;//ewgfaw
    public bool bIsMoving;
    #endregion
    #endregion

    [FormerlySerializedAs("RaycastPosition")]
    [Header("checks")]
    [SerializeField] private Transform raycastPosition;
    [FormerlySerializedAs("GroundLayer")] [SerializeField] private LayerMask gGroundLayer;
    [SerializeField] private bool bCanDebug;

    public Rigidbody2D rb2D;
    public bool bIsGrounded;

    #region --States---
    public playerState_Idle IdleState { get; private set; }
    public playerState_Air AirState  { get; private set; }
    public playerState_Move MoveState { get; private set; }
    public playerState_Jump JumpState { get; private set; }

    #endregion

    private void Awake()
    {
        PlayerStateManager = new playerStateManager();
        PlayerStateManager.playerCharacter = this;

        IdleState = new playerState_Idle(this, PlayerStateManager);
        AirState = new playerState_Air(this, PlayerStateManager);
        MoveState = new playerState_Move(this, PlayerStateManager);
        JumpState = new playerState_Jump(this, PlayerStateManager);

        rb2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        //PlayerInput.onActionTriggered += Jump;

        //store original values
        originalGravityScale = rb2D.gravityScale;
        originalJumpBufferTimer = jumpBufferTimer;
        originalLinearDamping = rb2D.linearDamping;
    }
    private void Start()
    {
        PlayerStateManager.Initialize(IdleState);
    }
    private void FixedUpdate()
    {
        bIsGrounded = Physics2D.Raycast(raycastPosition.position, Vector2.down, 0.05f, gGroundLayer);     //ray cast for check grounded
        PlayerStateManager.CurrentState.Update();
    }

    public IEnumerator MoveUpdate()
    {
        while (bIsMoving)
        {
            if (bCanDebug) { Log.Green("moving"); }

            yield return new WaitForFixedUpdate();
            rb2D.linearVelocity = new Vector2(movementInput * moveSpeed, rb2D.linearVelocity.y);
        }
    }
    public IEnumerator GroundCheckUpdate()
    {
        while (!bIsGrounded)
        {
            if (bCanDebug) { Log.Blue("in Air"); }
            if (bIsGrounded)
            {
                if (bCanDebug) { Log.Yellow("stops"); }
                //StopCoroutine(Cmove);
                //rb2D.linearVelocity = new Vector2(0, 0);
            }

            yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator InputBufferUpdate()
    {
        jumpBufferTimer = originalJumpBufferTimer;
        while (bIsInputbuffer)
        {
            if (bCanDebug) { Log.Blue("input buffer"); }
            jumpBufferTimer -= Time.deltaTime;
            InputBuffer();
            yield return null;
        }
        jumpBufferTimer = originalJumpBufferTimer;
    }
    public void InputBuffer()
    {
        // do normal jump if buffer timer is > 0 when player is grounded
        if (jumpBufferTimer > 0 && bIsGrounded && !bShortJump)
        {
            if (bCanDebug) { Log.Green("input buffer Jump"); }
            PlayerStateManager.ChangeState(JumpState);
        }
        else if (jumpBufferTimer > 0 && bIsGrounded && bShortJump)
        {
            if (bCanDebug) { Log.Yellow("input buffer Short Jump"); }
            PlayerStateManager.ChangeState(JumpState);
            bShortJump = false;
        }
        else if (jumpBufferTimer < 0)
        {
            bIsInputbuffer = false;
            bShortJump = false;
            if (bCanDebug) { Log.Yellow("Input buffer time out"); }
        }
    }
}