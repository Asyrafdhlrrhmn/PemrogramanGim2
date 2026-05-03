using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;

    [Header("Target")]
    public Transform player;
    public float chaseDistance = 5f;

    [Header("Patrol")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.35f; // sedikit diperbesar
    public LayerMask groundLayer;

    [Header("Visual")]
    public Transform visual;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool movingRight = true;
    private bool isGrounded;

    private float moveDir = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = visual.GetComponent<Animator>();
        sr = visual.GetComponent<SpriteRenderer>();

        // auto cari player
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogError("Player tidak ditemukan! Pastikan tag = Player");
        }
    }

    void Update()
    {
        // ✅ ground check lebih stabil
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (player == null) return;

        float distance = Mathf.Abs(player.position.x - transform.position.x);

        // ❗ FIX: jangan langsung return (biar tidak getar)
        if (!isGrounded)
        {
            moveDir = 0;
        }
        else
        {
            // ===== LOGIC GERAK =====
            if (distance < chaseDistance)
            {
                moveDir = Mathf.Sign(player.position.x - transform.position.x);
            }
            else
            {
                moveDir = movingRight ? 1 : -1;

                if (movingRight && transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
                else if (!movingRight && transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }
        }

        // ===== ANIMASI =====
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // ===== FLIP =====
        if (moveDir != 0)
            sr.flipX = moveDir < 0;
    }

    void FixedUpdate()
    {
        // ✅ physics di sini (stabil)
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (leftPoint != null && rightPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(leftPoint.position, rightPoint.position);
        }
    }
}