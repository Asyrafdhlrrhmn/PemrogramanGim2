using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameOverUI gameOverUI;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOverUI.ShowGameOver();
        }
    }
}
