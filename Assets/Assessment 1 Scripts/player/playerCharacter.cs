using UnityEngine;


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
    public bool bIsInputbuffer;
    public bool bShortJump;
    public bool bJumpGravityReset;
    public bool bCanJump;
    //Original Value
    public float originalJumpBufferTimer { get; private set; }
    public float originalGravityScale { get; private set; }
    public float originalLinearDamping;

    [Header("Coyote Time Settings")]
    public float coyoteTimeTimer;
    public float originalCoyoteTimeTimer;
    public bool bCanCoyoteJump = false;

    #endregion
    #region --Move Settings---
    [Header("Move settings")]
    public float moveSpeed = 2.5f;
    public float MaxMoveSpeed = 10f;
    public float MaxFallingSpeed = 25f;
    public Vector2 Movedirection;

    [Header("Movement other settings")]
    public float ApplyLandingDamping = 0.8f;
    public bool BCanOriginalValuesReset;
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
        bIsInputbuffer = false;
        bShortJump = false;
        bJumpGravityReset = false;
        BCanOriginalValuesReset = false;
        bCanJump = true;
        //
    }
    private void Start()
    {

    }
    private void FixedUpdate()
    {
        bIsGrounded = Physics2D.Raycast(raycastPosition.position, Vector2.down, 0.075f, gGroundLayer);     //ray cast for check grounded
    }
    public void originalValuesReset()
    {
        jumpBufferTimer = originalJumpBufferTimer;
        rb2D.gravityScale = originalGravityScale;
        rb2D.linearDamping = originalLinearDamping;
    }


}