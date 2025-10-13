using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

#region --Debug---
public static class Log
{
    public static void red(string msg) => Debug.Log($" <color=red>{msg}</color>");
    public static void yellow(string msg) => Debug.Log($" <color=yellow>{msg}</color>");
    public static void Green(string msg) => Debug.Log($" <color=lime>{msg}</color>");
    public static void Blue(string msg) => Debug.Log($" <color=cyan>{msg}</color>");
    public static void purple(string msg) => Debug.LogError($" <color=violet>{msg}</color>");
}
#endregion

public class playerCharacter : MonoBehaviour
{
    int m_CurrentHealth = 100;
    public event Action OnPlayerDead;

    public void TakeDamage(int damage)
    {
        m_CurrentHealth -= damage;

        if (m_CurrentHealth <= 0)
        {
            OnPlayerDead?.Invoke();
        }
    }

    public playerStateManager playerStateManager { get; private set; }
    private PlayerInput playerInput;

    #region --movement settings---
    #region --JUMP Settings---
    [Header("Jump Settings")]
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public float JumpGravity = 5f;
    [SerializeField] public float JumpDownForce = 5f;
    [SerializeField] public float JumpBufferTimer = 0.2f;
    [SerializeField] public float JumpLinearDamping = 0;
    public float originalJumpBufferTimer;
    public float originalGravityScale;
    public bool bIsInputbuffer = false;
    public bool bShortJump = false;
    public float originalLinearDamping;
    public Coroutine CGroundUpdate;
    public bool bJumpGravityReset = true;
    #endregion

    #region --Move Settings---
    [Header("Move settings")]
    [SerializeField] private float moveSpeed = 5f;
    public float movementInput;
    public Coroutine Cmove;
    public bool IsMoving;
    #endregion
    #endregion


    [Header("checks")]
    [SerializeField] private Transform RaycastPosition;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private bool bCanDebug;

    public Rigidbody2D rb2D;
    public bool bIsGrounded;

    #region --States---
    public player_StateIdle idleState { get; private set; }
    public player_StateAir airState  { get; private set; }
    public player_StateMove moveState { get; private set; }
    public player_StateJump jumpState { get; private set; }

    #endregion

    private void Awake()
    {
        playerStateManager = new playerStateManager();
        playerStateManager.playerCharacter = this;

        idleState = new player_StateIdle(this, playerStateManager);
        airState = new player_StateAir(this, playerStateManager);
        moveState = new player_StateMove(this, playerStateManager);
        jumpState = new player_StateJump(this, playerStateManager);

        rb2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        //PlayerInput.onActionTriggered += Jump;

        //store original values
        originalGravityScale = rb2D.gravityScale;
        originalJumpBufferTimer = JumpBufferTimer;
        originalLinearDamping = rb2D.linearDamping;
    }
    private void Start()
    {
        playerStateManager.Initialize(new player_StateMove(this, playerStateManager));
    }
    private void FixedUpdate()
    {
        bIsGrounded = Physics2D.Raycast(RaycastPosition.position, Vector2.down, 0.05f, GroundLayer);     //ray cast for check grounded
        playerStateManager.currentState.Update();
    }

    public IEnumerator MoveUpdate()
    {
        while (IsMoving)
        {
            if (bCanDebug) { Log.Green("moveing"); }

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
                if (bCanDebug) { Log.yellow("stops"); }
                //StopCoroutine(Cmove);
                //rb2D.linearVelocity = new Vector2(0, 0);
            }

            yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator inputBufferUpdate()
    {
        JumpBufferTimer = originalJumpBufferTimer;
        while (bIsInputbuffer)
        {
            if (bCanDebug) { Log.Blue("input buffer"); }
            JumpBufferTimer -= Time.deltaTime;
            inputBuffer();
            yield return null;
        }
        JumpBufferTimer = originalJumpBufferTimer;
    }
    public void inputBuffer()
    {
        // do normal jump if buffer timer is > 0 when player is grounded
        if (JumpBufferTimer > 0 && bIsGrounded && !bShortJump)
        {
            if (bCanDebug) { Log.Green("input buffer Jump"); }
            playerStateManager.ChangeState(jumpState);
            //Jump(JumpGravity, jumpForce, true);
        }
        else if (JumpBufferTimer > 0 && bIsGrounded && bShortJump)
        {
            if (bCanDebug) { Log.yellow("input buffer Short Jump"); }
            //Jump(originalGravityScale, jumpForce, true);
            bShortJump = false;
        }
        else if (JumpBufferTimer < 0)
        {
            bIsInputbuffer = false;
            bShortJump = false;
            if (bCanDebug) { Log.yellow("Input buffer time out"); }
        }
    }
}