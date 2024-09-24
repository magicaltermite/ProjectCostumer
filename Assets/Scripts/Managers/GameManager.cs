using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace Managers {
public class GameManager : MonoBehaviour {
    public bool isPaused { get; set; }
    [SerializeField] private bool hasIntroDialogue;
    [SerializeField] private string filePath;
    [SerializeField] private GameObject owlInField;
    [SerializeField] private GameObject owlOnHead;

    private int clueCounter;

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void Start() {
        if (hasIntroDialogue) {
            IntroDialogue(filePath);
        }
    }

    public void Update() {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape)) {
            Pausing(true);
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape)) {
            Pausing(false);
        }
        
    }

    public void IncrementClueCounter() {
        clueCounter++;
        Debug.Log(clueCounter);

        if (clueCounter >= 4) {
            LoadScene("ChoiceScene", false);
        }
    }

    public void CaptureMouse(bool isCaptured) {
        Cursor.lockState = isCaptured ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void LoadScene(string sceneName, bool captureMouse) {
        CaptureMouse(captureMouse);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    
    
    public void Pausing(bool pausing) {
        isPaused = pausing;
        UIManager.Instance.PauseGame(isPaused);
    }

    private void IntroDialogue(string filePath) {
        UIManager.Instance.EnableDialogueBox(true, false, filePath);
        
    }

    public void ChoiceSceneIntro() {
        UIManager.Instance.EnableDialogueBox(true, false, "/Dialogue/ChoiceIntroDialogue.txt");
    }

    
    
    // For controlling the choice in the choice scene
    public void PickChoice(bool correctChoice) {
        UIManager.Instance.EnableDialogueBox(true, false,
            correctChoice ? "/Dialogue/ChoiceDialogueCorrect.txt" : "/Dialogue/ChoiceDialogueWrong.txt");
    }

    public void PutOwlOnHead() {
        owlInField.SetActive(false);
        owlOnHead.SetActive(true);
    }

}
    
}

