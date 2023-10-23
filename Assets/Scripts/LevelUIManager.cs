using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    public Button exitButton;

    public Text scoreText;
    private int score;

    private void Start()
    {
        InitializeButton();
        SetScoreText(0);
    }

    private void InitializeButton()
    {
        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
        }
    }

    public void SetScoreText(int addScore)
    {
        score += addScore;

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        scoreText.text = "Score: " + score;
    }
}
