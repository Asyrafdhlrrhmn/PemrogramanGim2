using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 9f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    public Transform visual;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded;
    private Vector2 moveInput; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = visual.GetComponent<Animator>();
        sr = visual.GetComponent<SpriteRenderer>();

        sr.flipX = false;
    }

    void Update()
    {
        // cek tanah
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // animasi
        float speed = Mathf.Abs(moveInput.x);
        speed = speed > 0 ? 1f : 0f;
        anim.SetFloat("Speed", speed);

        // flip karakter
        if (moveInput.x > 0)
            sr.flipX = false;
        else if (moveInput.x < 0)
            sr.flipX = true;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("MOVE: " + moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}