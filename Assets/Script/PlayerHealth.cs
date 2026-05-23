using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;

    [Header("Sound")]
    public AudioClip hurtSound;

    public AudioClip healSound;

    private int currentHealth;

    private Animator anim;

    private bool isDead;

    private PlayerController playerController;

    private PlayerAttack playerAttack;

    private Rigidbody2D rb;

    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;

        anim =
            GetComponentInChildren<Animator>();

        playerController =
            GetComponent<PlayerController>();

        playerAttack =
            GetComponent<PlayerAttack>();

        rb =
            GetComponent<Rigidbody2D>();

        audioSource =
            GetComponent<AudioSource>();
    }

    // =========================
    // DAMAGE + KNOCKBACK
    // =========================
    public void TakeDamage(
        int damage,
        Vector2 knockback
    )
    {
        // cegah damage saat mati
        if (isDead) return;

        currentHealth -= damage;

        Debug.Log(
            "Player HP: " + currentHealth
        );

        // knockback
        if (rb != null)
        {
            rb.velocity = knockback;
        }

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

        // check death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // =========================
    // DAMAGE NORMAL
    // =========================
    public void TakeDamage(int damage)
    {
        // cegah damage saat mati
        if (isDead) return;

        currentHealth -= damage;

        Debug.Log(
            "Player HP: " + currentHealth
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

        // check death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // =========================
    // HEAL
    // =========================
    public void Heal(int amount)
    {
        // cegah heal saat mati
        if (isDead) return;

        currentHealth += amount;

        // batas max HP
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log(
            "Player Heal: " + currentHealth
        );

        // heal sound
        if (audioSource != null &&
            healSound != null)
        {
            audioSource.PlayOneShot(
                healSound
            );
        }
    }

    // =========================
    // DIE
    // =========================
    void Die()
    {
        isDead = true;

        Debug.Log("Player Mati!");

        // stop movement
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // stop attack
        if (playerAttack != null)
        {
            playerAttack.enabled = false;
        }

        // stop physics
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // death animation
        if (anim != null)
        {
            anim.SetTrigger("Dead");
        }

        // game over
        Invoke(nameof(GameOver), 1.5f);
    }

    void GameOver()
    {
        gameObject.SetActive(false);

        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
        }
    }

    public int CurrentHealth()
    {
        return currentHealth;
    }
}