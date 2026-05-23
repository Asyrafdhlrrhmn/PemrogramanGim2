using UnityEngine;

public class JumpPlant : MonoBehaviour
{
    public float jumpForce = 18f;

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb =
                other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // reset velocity
                rb.velocity =
                    new Vector2(
                        rb.velocity.x,
                        0
                    );

                // launch player
                rb.AddForce(
                    Vector2.up * jumpForce,
                    ForceMode2D.Impulse
                );
            }
        }
    }
}