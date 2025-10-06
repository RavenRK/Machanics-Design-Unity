using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public static class Log
{
    public static void red(string msg) => Debug.Log($" <color=red>{msg}</color>");
    public static void yellow(string msg) => Debug.Log($" <color=yellow>{msg}</color>");
    public static void Green(string msg) => Debug.Log($" <color=lime>{msg}</color>");
    public static void Blue(string msg) => Debug.Log($" <color=cyan>{msg}</color>");
    public static void purple(string msg) => Debug.LogError($" <color=violet>{msg}</color>");
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float JumpGravity = 5f;
    [SerializeField] private float JumpDownForce = 5f;
    [SerializeField] private float JumpBufferTimer = 0.2f;
    [SerializeField] private float JumpLinearDamping = 0;
    private float originalJumpBufferTimer;
    private float originalGravityScale;
    private bool bIsInputbuffer = false;
    private bool bShortJump = false;
    private float originalLinearDamping;
    private Coroutine CGroundUpdate;

    [Header("Move settings")]
    [SerializeField] private float moveSpeed = 5f;
    private float movementInput;
    private Coroutine Cmove;

    [Header("checks")]
    [SerializeField] private Transform m_RaycastPosition;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private bool bCanDebug;


    bool bIsGrounded;
    bool bJumpGravityReset= true;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        //store original values
        originalGravityScale = rb2D.gravityScale;   
        originalJumpBufferTimer = JumpBufferTimer;
        originalLinearDamping = rb2D.linearDamping;
    }
    private void Start()
    {

    }
    private void FixedUpdate()
    {
        bIsGrounded = Physics2D.Raycast(m_RaycastPosition.position, Vector2.down, 0.05f, m_GroundLayer);     //ray cast for check grounded

        if (bIsInputbuffer) { JumpBufferTimer -= Time.deltaTime; inputBuffer(); }       // jump input buffer
    }

    #region --JUMP---
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed )  //button down
        {
            if (bIsGrounded && !bIsInputbuffer)
            {
                if (bCanDebug) { Log.Green("Jump"); }
                Jump(JumpGravity, jumpForce, true);
            }
            else
            {
                JumpBufferTimer = originalJumpBufferTimer;     //reset input buffer timer
                bIsInputbuffer = true;                         //allow tick for inputbuffer timer
            }
        }

        if (context.canceled && bJumpGravityReset)              //buttom up
        {
            if (bCanDebug) { Log.Green("Fall"); }
            Jump(originalGravityScale, JumpDownForce, false);   //reduce gravity and add down force
        }
        else if (context.canceled && bIsInputbuffer)
        {
            bShortJump = true;
        }

    }
    public void Jump(float NewGracityScale, float NewJumpPower, bool allowGracityReset)
    {
        rb2D.AddForce(Vector2.up * NewJumpPower, ForceMode2D.Impulse);
        rb2D.gravityScale = NewGracityScale;
        bJumpGravityReset = allowGracityReset;                          //should we let the player recast Down force at end Jump

        rb2D.linearDamping = JumpLinearDamping;
        CGroundUpdate = StartCoroutine(GroundCheckUpdate());
    }
    public void inputBuffer()
    {
        if (JumpBufferTimer > 0 && bIsGrounded && !bShortJump)
        {
            if (bCanDebug) { Log.Green("input buffer Jump"); }
            Jump(JumpGravity, jumpForce, true);
        }
        else if (JumpBufferTimer > 0 && bIsGrounded && bShortJump)
        {
            if (bCanDebug) { Log.yellow("input buffer Short Jump"); }
            Jump(originalGravityScale, jumpForce, true);
            bShortJump = false;
        }
        else if (JumpBufferTimer < 0)
        {
            bIsInputbuffer = false;
            bShortJump = false;
            if (bCanDebug) {Log.yellow("Input buffer time out"); }
        }
    }
    #endregion


    bool m_IsMoving;
    public void move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (bCanDebug) { Log.Green("move"); }

            m_IsMoving = true;

            movementInput = context.ReadValue<float>();
            Cmove = StartCoroutine(MoveUpdate());
        }

        if (context.canceled)
        {
            if (bCanDebug) { Log.yellow("stop"); }

            m_IsMoving = false;

            movementInput = 0;

            if (bIsGrounded)
            rb2D.linearVelocity = new Vector2(0, 0);
        }
    }

    private IEnumerator MoveUpdate()
    {
        while (m_IsMoving)
        {
            if (bCanDebug) { Log.Green("moveing"); }

            yield return new WaitForFixedUpdate();
            rb2D.linearVelocity = new Vector2(movementInput * moveSpeed, rb2D.linearVelocity.y); 
        }
    }
    private IEnumerator GroundCheckUpdate()
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
}
