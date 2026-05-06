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
    public Transform groundAheadCheck; 
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheck; 
    public float wallCheckDistance = 0.2f;

    [Header("Visual")]
    public Transform visual;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool movingRight = true;
    private float moveDir = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = visual.GetComponent<Animator>();
        sr = visual.GetComponent<SpriteRenderer>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
            movingRight = player.position.x > transform.position.x;
        }
    }

    void Update()
    {
        if (player == null) return;

        bool isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        bool hasGroundAhead = Physics2D.OverlapCircle(
            groundAheadCheck.position,
            groundCheckRadius,
            groundLayer
        );

        bool hitWall = Physics2D.Raycast(
            wallCheck.position,
            movingRight ? Vector2.right : Vector2.left,
            wallCheckDistance,
            groundLayer
        );

        float distance = Mathf.Abs(player.position.x - transform.position.x);

        if (!isGrounded)
        {
            moveDir = 0;
            return;
        }

        // ===== CHASE =====
        if (distance < chaseDistance)
        {
            float dir = Mathf.Sign(player.position.x - transform.position.x);

            if (!hasGroundAhead || hitWall)
            {
                moveDir = 0;
            }
            else
            {
                moveDir = dir;
                movingRight = dir > 0;
            }
        }

        else
        {
            float dir = movingRight ? 1 : -1;

            // 🔥 balik kalau mentok / mau jatuh
            if (!hasGroundAhead || hitWall)
            {
                movingRight = !movingRight;
                dir = movingRight ? 1 : -1;
            }

            moveDir = dir;

            if (movingRight && transform.position.x > rightPoint.position.x)
                movingRight = false;

            if (!movingRight && transform.position.x < leftPoint.position.x)
                movingRight = true;
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (moveDir != 0)
            sr.flipX = moveDir < 0;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (groundAheadCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundAheadCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(
                wallCheck.position,
                wallCheck.position + Vector3.right * (movingRight ? 0.2f : -0.2f)
            );
        }
    }
}