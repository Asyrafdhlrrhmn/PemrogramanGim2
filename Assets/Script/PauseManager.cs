using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject settingsPanel;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
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

            if (settingsPanel != null)
            {
                settingsPanel.SetActive(false);
            }

            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        isPaused = false;

        pausePanel.SetActive(false);

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettings()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }
}