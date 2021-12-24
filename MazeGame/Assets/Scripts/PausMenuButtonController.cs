using UnityEngine;
using UnityEngine.SceneManagement;

public class PausMenuButtonController : MonoBehaviour
{

    [Header("Paus Menu and its attributes")]
    public GameObject pauseMenu;
    bool isPauseButtonClicked = false;
    public static bool gameIsPaused = false;

    void Update()
    {
        // if pause button clicked and game wasn't paused 
        // we pause it and vice versa 
        if (isPauseButtonClicked && GameObject.FindGameObjectWithTag("DIalogueInstruction") == null)
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }

        // clicke once
        isPauseButtonClicked = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void onPauseButtonClicked()
    {
        isPauseButtonClicked = true;
    }
}
