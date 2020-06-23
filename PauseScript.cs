using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false;
    public Button flipperSwitchButton;
    public Button pauseButton;

    public GameObject pauseMenuUI;

    public void PauseGame()
    {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            flipperSwitchButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        flipperSwitchButton.gameObject.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void ReplayLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1;
    }
}
