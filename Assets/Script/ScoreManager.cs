using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI highScoreText;

    private int score;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // load current score
        score =
            PlayerPrefs.GetInt(
                "CurrentScore",
                0
            );

        UpdateUI();

        // load high score
        int highScore =
            PlayerPrefs.GetInt(
                "HighScore",
                0
            );

        // tampilkan high score
        if (highScoreText != null)
        {
            highScoreText.text =
                "High Score : " +
                highScore;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        // save current score
        PlayerPrefs.SetInt(
            "CurrentScore",
            score
        );

        // cek high score
        int highScore =
            PlayerPrefs.GetInt(
                "HighScore",
                0
            );

        // kalau score baru lebih besar
        if (score > highScore)
        {
            // save high score
            PlayerPrefs.SetInt(
                "HighScore",
                score
            );

            // update high score UI
            if (highScoreText != null)
            {
                highScoreText.text =
                    "High Score : " +
                    score;
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Score : " + score;
        }
    }

    // reset current score
    public void ResetScore()
    {
        score = 0;

        PlayerPrefs.DeleteKey(
            "CurrentScore"
        );

        UpdateUI();
    }
}