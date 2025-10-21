using UnityEngine;
using System.Collections;
using UnityEngine.Events;

#region --Debug---
public static class Log
{
    public static void Red(string msg) => Debug.Log($" <color=red>{msg}</color>");
    public static void Yellow(string msg) => Debug.Log($" <color=yellow>{msg}</color>");
    public static void Green(string msg) => Debug.Log($" <color=lime>{msg}</color>");
    public static void Blue(string msg) => Debug.Log($" <color=cyan>{msg}</color>");
}
#endregion

public class playerCharacter : MonoBehaviour
{
    #region --JUMP Settings---
    [Header("basic Jump Settings")]
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public float jumpGravity = 5f;
    [SerializeField] public float jumpDownForce = 5f;
    [SerializeField] public float jumpLinearDamping = 0.5f;

    [Header("JumpBuffer Settings")]
    [SerializeField] public float jumpBufferTimer = 0.2f;

    public bool bJumpGravityReset;

    //Original Values
    public float originalJumpBufferTimer { get; private set; }
    public float originalGravityScale { get; private set; }
    public float originalLinearDamping;

    #endregion
    #region --Move Settings---
    [Header("Move settings")]
    public float moveSpeed = 5f;
    public Vector2 Movedirection;

    #endregion

    [Header("checks")]
    public Transform raycastPosition;
    public LayerMask gGroundLayer;
    public bool bIsGrounded;

    public Rigidbody2D rb2D {get; private set; }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        //store original values
        originalGravityScale = rb2D.gravityScale;
        originalLinearDamping = rb2D.linearDamping;
        originalJumpBufferTimer = jumpBufferTimer;
        //bIsInputbuffer = false;
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



}