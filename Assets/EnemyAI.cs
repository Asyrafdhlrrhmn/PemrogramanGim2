using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;

    [Header("Target")]
    public Transform player;

    public float chaseDistance = 5f;

    [Header("Attack")]
    public float attackDistance = 1.2f;

    public float attackCooldown = 0.4f;

    public Transform attackPoint;

    public float attackRadius = 0.5f;

    public int damage = 10;

    public LayerMask playerLayer;

    [Header("Patrol")]
    public Transform leftPoint;

    public Transform rightPoint;

    [Header("Ground Check")]
    public Transform groundCheck;

    public float groundCheckRadius = 0.2f;

    public LayerMask groundLayer;

    [Header("Visual")]
    public Transform visual;

    [Header("Effect")]
    public GameObject slashEffect;

    [Header("Sound")]
    public AudioClip attackSound;

    private Rigidbody2D rb;

    private Animator anim;

    private SpriteRenderer sr;

    private AudioSource audioSource;

    private bool movingRight = true;

    private float moveDir;

    private bool isAttacking;

    private float attackTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim =
            visual.GetComponent<Animator>();

        sr =
            visual.GetComponent<SpriteRenderer>();

        audioSource =
            GetComponent<AudioSource>();

        GameObject p =
            GameObject.FindGameObjectWithTag(
                "Player"
            );

        if (p != null)
        {
            player = p.transform;
        }

        if (slashEffect != null)
        {
            slashEffect.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;

        float distance =
            Vector2.Distance(
                transform.position,
                player.position
            );

        // =========================
        // ATTACK
        // =========================
        if (distance <= attackDistance)
        {
            moveDir = 0;

            movingRight =
                player.position.x >
                transform.position.x;

            if (!isAttacking &&
                attackTimer <= 0)
            {
                Attack();
            }
        }

        // =========================
        // CHASE
        // =========================
        else if (distance <= chaseDistance)
        {
            if (!isAttacking)
            {
                float dir =
                    Mathf.Sign(
                        player.position.x -
                        transform.position.x
                    );

                movingRight = dir > 0;

                moveDir = dir;
            }
        }

        // =========================
        // PATROL
        // =========================
        else
        {
            if (!isAttacking)
            {
                moveDir =
                    movingRight ? 1 : -1;

                if (movingRight &&
                    transform.position.x >
                    rightPoint.position.x)
                {
                    movingRight = false;
                }

                if (!movingRight &&
                    transform.position.x <
                    leftPoint.position.x)
                {
                    movingRight = true;
                }
            }
        }

        // =========================
        // FLIP SPRITE
        // =========================
        if (sr != null)
        {
            sr.flipX = !movingRight;
        }

        // =========================
        // ATTACK POINT
        // =========================
        if (attackPoint != null)
        {
            Vector3 pos =
                attackPoint.localPosition;

            pos.x =
                movingRight
                ? Mathf.Abs(pos.x)
                : -Mathf.Abs(pos.x);

            attackPoint.localPosition = pos;
        }

        // =========================
        // SLASH EFFECT
        // =========================
        if (slashEffect != null)
        {
            Vector3 slashPos =
                slashEffect.transform.localPosition;

            slashPos.x =
                movingRight
                ? Mathf.Abs(slashPos.x)
                : -Mathf.Abs(slashPos.x);

            slashEffect.transform.localPosition =
                slashPos;

            SpriteRenderer slashSR =
                slashEffect.GetComponent<SpriteRenderer>();

            if (slashSR != null)
            {
                slashSR.flipX = !movingRight;
            }
        }

        // =========================
        // ANIMATION
        // =========================
        if (!isAttacking)
        {
            anim.SetFloat(
                "Speed",
                Mathf.Abs(moveDir)
            );
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            rb.velocity =
                Vector2.zero;

            return;
        }

        rb.velocity =
            new Vector2(
                moveDir * speed,
                rb.velocity.y
            );
    }

    void Attack()
    {
        isAttacking = true;

        moveDir = 0;

        rb.velocity = Vector2.zero;

        // attack sound
        if (audioSource != null &&
            attackSound != null)
        {
            audioSource.PlayOneShot(
                attackSound
            );
        }

        // animation
        anim.ResetTrigger("Attack");

        anim.SetTrigger("Attack");

        attackTimer = attackCooldown;

        // slash effect
        if (slashEffect != null)
        {
            slashEffect.SetActive(true);

            Invoke(nameof(HideSlash), 0.2f);
        }

        // hit player
        Collider2D hitPlayer =
            Physics2D.OverlapCircle(
                attackPoint.position,
                attackRadius,
                playerLayer
            );

        if (hitPlayer != null)
        {
            PlayerHealth ph =
                hitPlayer.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.TakeDamage(damage);
            }
        }

        Invoke(
            nameof(StopAttack),
            attackCooldown
        );
    }

    void HideSlash()
    {
        if (slashEffect != null)
        {
            slashEffect.SetActive(false);
        }
    }

    void StopAttack()
    {
        isAttacking = false;
    }

    void OnDrawGizmos()
    {
        // Ground Check
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }

        // Attack Distance
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            attackDistance
        );

        // Attack Point
        if (attackPoint != null)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireSphere(
                attackPoint.position,
                attackRadius
            );
        }
    }
}