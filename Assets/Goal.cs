using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        Debug.Log("KENA PORTAL");

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER MASUK");

            GameManager.instance.WinGame();
        }
    }
}