using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameOverScreen;

    [Header("Settings")]
    public float gameOverDelay = 1f;

    void Start()
    {
         if (gameOverScreen != null) gameOverScreen.SetActive(false);
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
}