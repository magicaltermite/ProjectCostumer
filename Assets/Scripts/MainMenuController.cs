using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{
    private void Start() {
        GameManager.Instance.CaptureMouse(false);
    }

    public void startButton()
    {
        GameManager.Instance.LoadScene("Oscar_New_Scene", true);
    }

    public void exitButton()
    {
        Application.Quit();

        // Debug here is to check if the button works, since the game does not actually quit in the editor
        Debug.Log("Game has been quit");
    }

}
