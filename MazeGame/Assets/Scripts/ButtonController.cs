using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// interractive buttons to control player 
// Torch and Game Over Buttons
public class ButtonController : MonoBehaviour
{

    PlayerController pc;

    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    // turn torch on
    public void onTurnTorchButtonClicked()
    {
        pc.turnTorchOn();
    }

    // turn torch off
    public void onTurnTorchOffButtonClicked()
    {
        pc.turnTorchOff();
    }

    // Quite Application 
    public void onQuitButtonClicked()
    {
        Application.Quit();
    }

    // Main Menu 
    public void onMenuButtonCLicked()
    {
     
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    // Restart Game
    public void onRestartButtonclicked()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
