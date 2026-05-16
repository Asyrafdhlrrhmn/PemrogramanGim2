using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    public int damage = 20;

    public float attackRadius = 0.5f;

    public LayerMask enemyLayer;

    public Transform attackPoint;

    [Header("Effect")]
    public GameObject slashEffect;

    public Transform slashEffectPoint;

    [Header("Sound")]
    public AudioClip attackSound;

    private Animator animator;

    private AudioSource audioSource;

    private bool isAttacking;

    void Start()
    {
        animator =
            GetComponentInChildren<Animator>();

        audioSource =
            GetComponent<AudioSource>();
    }

    public void OnAttack(
        InputAction.CallbackContext context
    )
    {
        if (context.performed && !isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;

        // play animation
        animator.SetTrigger("Attack");

        // play sound
        if (audioSource != null &&
            attackSound != null)
        {
            audioSource.PlayOneShot(
                attackSound
            );
        }

        // slash effect
        if (slashEffect != null)
        {
            Instantiate(
                slashEffect,
                slashEffectPoint.position,
                Quaternion.identity
            );
        }

        // detect enemy
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRadius,
                enemyLayer
            );

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy =
                hit.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        // reset attack
        Invoke(nameof(ResetAttack), 0.4f);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRadius
        );
    }
}