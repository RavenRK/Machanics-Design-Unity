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
    #region --movement settings---
    #region --JUMP Settings---
    [Header("basic Jump Settings")]
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public float jumpGravity = 5f;
    [SerializeField] public float jumpDownForce = 5f;
    [SerializeField] public float jumpLinearDamping = 0.5f;

    [Header("JumpBuffer Settings")]
    [SerializeField] public float jumpBufferTimer = 0.2f;
    public bool bIsInputbuffer { get; private set; } 
    public bool bShortJump { get; private set; }
    public bool bJumpGravityReset;

    //Original Values
    public float originalJumpBufferTimer { get; private set; }
    public float originalGravityScale { get; private set; }
    public float originalLinearDamping;

    public Coroutine CGroundUpdate; //rhzhaw
    #endregion
    #region --Move Settings---
    [Header("Move settings")]
    public float moveSpeed = 5f;
    public float movementInput { get; private set; }

    #endregion
    #endregion

    [Header("checks")]
    [SerializeField] private Transform raycastPosition;
    [SerializeField] private LayerMask gGroundLayer;
    [SerializeField] private bool bCanDebug;

    public Rigidbody2D rb2D {get; private set; }
    public bool bIsGrounded { get; private set; }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        //store original values
        originalGravityScale = rb2D.gravityScale;
        originalLinearDamping = rb2D.linearDamping;
        originalJumpBufferTimer = jumpBufferTimer;
        bIsInputbuffer = false;
        bShortJump = false;
        bJumpGravityReset = false;
        //
    }
    private void Start()
    {

    }
    private void FixedUpdate()
    {
        bIsGrounded = Physics2D.Raycast(raycastPosition.position, Vector2.down, 0.05f, gGroundLayer);     //ray cast for check grounded

    }
    public IEnumerator GroundCheckUpdate()
    {
        while (!bIsGrounded)
        {
            if (bCanDebug) { Log.Blue("in Air"); }
            if (bIsGrounded)
            {
                if (bCanDebug) { Log.Yellow("stops"); }

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
            //InputBuffer();
            yield return null;
        }
        jumpBufferTimer = originalJumpBufferTimer;
    }
    //public void InputBuffer()
    //{
    //    // do normal jump if buffer timer is > 0 when player is grounded
    //    if (jumpBufferTimer > 0 && bIsGrounded && !bShortJump)
    //    {
    //        if (bCanDebug) { Log.Green("input buffer Jump"); }
    //        StateManager.ChangeState(JumpState);
    //    }
    //    else if (jumpBufferTimer > 0 && bIsGrounded && bShortJump)
    //    {
    //        if (bCanDebug) { Log.Yellow("input buffer Short Jump"); }
    //        StateManager.ChangeState(JumpState);
    //        bShortJump = false;
    //    }
    //    else if (jumpBufferTimer < 0)
    //    {
    //        bIsInputbuffer = false;
    //        bShortJump = false;
    //        if (bCanDebug) { Log.Yellow("Input buffer time out"); }
    //    }
    //}
}