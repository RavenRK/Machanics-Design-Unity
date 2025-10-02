using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpGravity = 5f;
    [SerializeField] private float pushDown = -5f;

    [SerializeField] private Transform raycastPosition;
    [SerializeField] private LayerMask groundLayer;

    bool bCanJump = true;
    bool bIsGrounded = true;

    [SerializeField] private float moveSpeed = 5f;

    private float originalGravityScale;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        originalGravityScale = rb2D.gravityScale;
    }
    private void FixedUpdate()
    {
        bIsGrounded = Physics2D.Raycast(raycastPosition.position, Vector2.down, 1f, groundLayer);
        //Debug.Log(rb2D.linearVelocity);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("we moveing right now");
        if (context.performed)
        {

        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (bCanJump && bIsGrounded)
        {
            if (context.performed)
            {
                Debug.Log("Jump");

                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb2D.gravityScale = jumpGravity;
                bCanJump = false;
            }
        }
        if (!bCanJump)
        {
            if (context.canceled)
            {
                Debug.Log("normal Grav");

                rb2D.AddForce(Vector2.up * pushDown, ForceMode2D.Impulse);
                rb2D.gravityScale = originalGravityScale;
                bCanJump = true;
            }
        }
    }
}
