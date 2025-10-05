using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float JumpGravity = 5f;
    [SerializeField] private float JumpDownForce = 5f;

    [Header("checks")]
    [SerializeField] private Transform m_RaycastPosition;
    [SerializeField] private LayerMask m_GroundLayer;

    [Header("Move settings")]
    [SerializeField] private float moveSpeed = 5f;
    private float movementInput;

    [SerializeField] private float originalGravityScale;
    bool bIsGrounded;
    bool bJumpGravityReset;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        originalGravityScale = rb2D.gravityScale;
    }
    private void FixedUpdate()
    {
        bIsGrounded = Physics2D.Raycast(m_RaycastPosition.position, Vector2.down, 0.1f, m_GroundLayer);
        rb2D.linearVelocity = new Vector2(movementInput * moveSpeed, rb2D.linearVelocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && bIsGrounded)
        {
            Debug.Log("Jump");
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb2D.gravityScale = JumpGravity;
            bJumpGravityReset = true;

        }
        if (context.canceled && bJumpGravityReset)
        {
            Debug.Log("fall");

            rb2D.AddForce(Vector2.up * JumpDownForce, ForceMode2D.Impulse);
            rb2D.gravityScale = originalGravityScale;
            bJumpGravityReset = false;
        }

    }
    public void move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("move");
            movementInput = context.ReadValue<float>();

        }
        //float movement = context.ReadValue<float>();
        //rb2D.linearVelocity = new Vector2(movement * moveSpeed, rb2D.linearVelocity.y);

        if (context.canceled && bIsGrounded)
        {
            Debug.Log("stop");
            movementInput = 0;
            //Vector2 StopVector = new Vector2(0, 0);
            //rb2D.linearVelocity = StopVector;
        }
    }
}
