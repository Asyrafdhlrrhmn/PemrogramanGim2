using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}