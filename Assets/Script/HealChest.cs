using UnityEngine;

public class HealChest : MonoBehaviour
{
    [Header("Heal")]
    public int healAmount = 30;

    [Header("Effect")]
    public GameObject heartEffect;

    private bool opened;

    private Animator anim;

    void Start()
    {
        anim =
            GetComponent<Animator>();

        // stop anim di frame awal
        if (anim != null)
        {
            anim.speed = 0;
        }
    }

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (other.CompareTag("Player")
            && !opened)
        {
            opened = true;

            // play chest animation
            if (anim != null)
            {
                anim.speed = 1;
            }

            // show heart
            if (heartEffect != null)
            {
                heartEffect.SetActive(true);
                Destroy(
                    heartEffect,
                    1f
                );
            }

            // heal player
            PlayerHealth ph =
                other.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.Heal(healAmount);
            }
        }
    }
}