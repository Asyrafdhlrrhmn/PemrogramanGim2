using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [Header("Next Scene")]
    public string nextSceneName;

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (other.CompareTag("Player"))
        {
            // load next stage
            SceneManager.LoadScene(
                nextSceneName
            );
        }
    }
}