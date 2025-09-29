using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float JumpGravity = 5f;
    [SerializeField] private float moveSpeed = 5f;
    private float originalGravityScale;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        originalGravityScale = rb2D.gravityScale;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        if (context.performed)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb2D.gravityScale = JumpGravity;
        }
        if (context.canceled)
        {
            rb2D.gravityScale = originalGravityScale;
        }

        
    }
}
