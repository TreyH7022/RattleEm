using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Button pressed");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Minigame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}