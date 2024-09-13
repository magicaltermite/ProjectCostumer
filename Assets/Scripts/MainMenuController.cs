using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{
    
    public void startButton()
    {
        // Reminder! if you want this to work, remember to set the main menu as scene 0 and the game scene/next scene as scene 1 in the build settings!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void exitButton()
    {
        Application.Quit();

        // Debug here is to check if the button works, since the game does not actually quit in the editor
        Debug.Log("Game has been quit");
    }

}
