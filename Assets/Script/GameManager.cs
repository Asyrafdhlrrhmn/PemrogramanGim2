using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public GameObject gameOverPanel;

    public GameObject winPanel;

    [Header("Sound")]
    public AudioClip gameOverSound;

    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource =
            GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        // tampilkan UI
        gameOverPanel.SetActive(true);

        // play sound
        if (audioSource != null &&
            gameOverSound != null)
        {
            audioSource.PlayOneShot(
                gameOverSound
            );
        }

        // pause game
        Invoke(nameof(PauseGame), 0.2f);
    }

    public void WinGame()
    {
        // sementara dulu
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Invoke(nameof(PauseGame), 0.2f);
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }
}