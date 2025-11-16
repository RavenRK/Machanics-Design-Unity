using JetBrains.Annotations;
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
    [SerializeField] public float ShortJumpForce = 10f;
    [SerializeField] public float jumpGravity = 5f;
    [SerializeField] public float jumpDownForce = 5f;
    [SerializeField] public float jumpLinearDamping = 0.5f;

    [Header("Jump Settings")]
    [SerializeField] public float jumpBufferTimer = 0.2f;
    public float originalCoyoteTimeTimer;
    public float apexGravityScale;
    public float apexSpeedBoost;

    [HideInInspector] public bool bIsInputbuffer;
    [HideInInspector] public bool bShortJump;
    [HideInInspector] public bool bJumpGravityReset;
    [HideInInspector] public bool bCanJump;
    [HideInInspector] public float coyoteTimeTimer;
    [HideInInspector] public bool bCanCoyoteJump = false;

    //Original Value
    public float originalJumpBufferTimer { get; private set; }
    public float originalGravityScale { get; private set; }
    [HideInInspector] public float originalLinearDamping;

    #endregion
    #region --Move Settings---
    [Header("Move settings")]
    public float moveSpeed = 2.5f;
    public float airMoveSpeed = 5f;
    [HideInInspector] public Vector2 Movedirection;

    [Header("Movement other settings")]
    public float ApplyLandingDamping = 0.8f;
    [HideInInspector] public bool BCanOriginalValuesReset;
    public CapsuleCollider2D playerCapsuleCollider;
    #endregion

    [Header("checks")]
    public LayerMask gGroundLayer;
    public Transform raycastPosition;
    [HideInInspector] public bool bIsGrounded;

    [SerializeField] public LayerMask interactableLayer;
    public Collider2D InteractionCollider;

    private HealthComponent healthComponent;
    private PlayerFeedBackManager feedbackM;
    public Rigidbody2D rb2D {get; private set; }

    public CircleCollider2D PinchingCollider;
    [HideInInspector] public Vector2 originalColliderSize;
    public Vector2 JumpColliderSize = new Vector2(.40f, .97f);

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
        feedbackM = GetComponentInChildren<PlayerFeedBackManager>();
        playerCapsuleCollider = GetComponentInChildren<CapsuleCollider2D>();

        //store original values
        BCanOriginalValuesReset = false;
        originalGravityScale = rb2D.gravityScale;
        originalJumpBufferTimer = jumpBufferTimer;
        originalLinearDamping = rb2D.linearDamping;
        originalColliderSize = playerCapsuleCollider.size;

        //Set defaults
        bCanJump = true;
        bShortJump = false;
        bIsInputbuffer = false;
        bJumpGravityReset = false;

        if (playerCapsuleCollider == null)
        { Log.Red("No Player Collider found in children"); }

        if (InteractionCollider == null)
        { Log.Red("No Interaction Collider found in children"); }
    }
    private void Start()
    {
        healthComponent.OnDamageTaken += OnplayerDamaged;
    }
    private void OnDestroy()
    {
        healthComponent.OnDamageTaken -= OnplayerDamaged;
    }
    private void OnplayerDamaged(float current, float max, float damage)
    {
        feedbackM.PlayDMGPlayerFeedBack();
    }
    private void FixedUpdate()
    {
        bIsGrounded = Physics2D.Raycast(raycastPosition.position, 
            Vector2.down, 0.05f, gGroundLayer);     //ground check
    }
    public void originalValuesReset()
    {
        jumpBufferTimer = originalJumpBufferTimer;
        rb2D.gravityScale = originalGravityScale;
        rb2D.linearDamping = originalLinearDamping;
    }


}