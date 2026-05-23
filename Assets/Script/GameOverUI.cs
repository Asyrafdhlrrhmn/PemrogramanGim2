using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void Restart()
    {
        // reset score
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetScore();
        }

        Time.timeScale = 1;

        SceneManager.LoadScene(
            "GameScene"
        );
    }

    public void BackToMenu()
    {
        // reset score
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetScore();
        }

        Time.timeScale = 1;

        SceneManager.LoadScene(
            "MainMenu"
        );
    }
}