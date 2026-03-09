using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;

    void Start()
    {
        Time.timeScale = 0f;

        startMenu.SetActive(true);
    }

    public void StartGame()
    {
        startMenu.SetActive(false);

        Time.timeScale = 1f;
    }
}