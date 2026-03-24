using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startMenu;
    public GameObject gameOverScreen;

    [Header("Settings")]
    public float gameOverDelay = 1f;

    void Start()
    {
        Time.timeScale = 0f;
         startMenu.SetActive(true);
         if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PlayerDied()
    {
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);

        if (gameOverScreen != null) 
            gameOverScreen.SetActive(true);

        Time.timeScale = 0f;  
    }
}