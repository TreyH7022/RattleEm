using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverScreen;

    public TMP_Text scoreText;
    public int score = 0;


    public float gameOverDelay = 1f;

    void Awake() 
    {
        instance = this;
    }
    void Start()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(false);

        UpdateScoreUI();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerDied()
    {
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSecondsRealtime(gameOverDelay);

        if (gameOverScreen != null) 
            gameOverScreen.SetActive(true);

        Time.timeScale = 0f;  
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}