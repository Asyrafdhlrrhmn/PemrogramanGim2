using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private float score = 0f;
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime * 10;
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        scoreText.text = "FINAL SCORE: " + Mathf.FloorToInt(score);
    }
}
