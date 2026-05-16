using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 50;

    [Header("Score")]
    public int scoreValue = 5;

    [Header("Coin Drop")]
    public GameObject coinPrefab;

    [Header("Sound")]
    public AudioClip hurtSound;

    public AudioClip deathSound;

    private int currentHealth;

    private Animator anim;

    private bool isDead;

    private EnemyAI enemyAI;

    private Rigidbody2D rb;

    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;

        anim =
            GetComponentInChildren<Animator>();

        enemyAI =
            GetComponent<EnemyAI>();

        rb =
            GetComponent<Rigidbody2D>();

        audioSource =
            GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        // cegah damage saat sudah mati
        if (isDead) return;

        currentHealth -= damage;

        Debug.Log(
            "Enemy HP: " + currentHealth
        );

        // hurt sound
        if (audioSource != null &&
            hurtSound != null)
        {
            audioSource.PlayOneShot(
                hurtSound
            );
        }

        // hurt animation
        if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }

        // cek mati
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // tambah score
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(
                scoreValue
            );
        }

        // stop AI
        if (enemyAI != null)
        {
            enemyAI.enabled = false;
        }

        // stop physics
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // death sound
        if (audioSource != null &&
            deathSound != null)
        {
            audioSource.PlayOneShot(
                deathSound
            );
        }

        // death animation
        if (anim != null)
        {
            anim.SetTrigger("Dead");
        }

        // spawn coin
        if (coinPrefab != null)
        {
            Instantiate(
                coinPrefab,
                transform.position +
                Vector3.up * 1f,
                Quaternion.identity
            );
        }

        // destroy enemy
        Destroy(gameObject, 1f);
    }
}