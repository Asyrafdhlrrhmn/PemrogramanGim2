using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 20;

    public Vector2 knockbackForce =
        new Vector2(0, 8f);

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player =
                other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(
                    damage,
                    knockbackForce
                );
            }
        }
    }
}