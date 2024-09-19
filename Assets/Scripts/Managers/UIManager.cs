using System;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Managers {
public class UIManager : MonoBehaviour {
    
    [SerializeField] private TMP_Text pickupPrompt;
    [SerializeField] private Image dialogueBox;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject cluesUI;
    public bool pause = false;
    private GameObject canvas;
    
    public static UIManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

        public void pauseGame(bool isPaused)
        {
                pauseMenu.SetActive(isPaused);
                cluesUI.SetActive(!isPaused);
                Debug.Log("Game paused");
                pause = isPaused;
        }

    private void Start() {
        pickupPrompt.gameObject.SetActive(false);
        
        
    }

    public void EnablePickupPrompt(bool isEnabled) {
        pickupPrompt.gameObject.SetActive(isEnabled);
    }

    public void EnableDialogueBox(bool isEnabled) {
        dialogueBox.gameObject.SetActive(isEnabled);
    }
    
}
}

public class DialogueWrapper {
    private string DialogueContents { get; }
    public string[] DialogueArray { get; private set; }

    public DialogueWrapper(string dialogueContents) {
        DialogueContents = dialogueContents;
    }

    public void PopulateArray() {
        DialogueArray = DialogueContents.Split(";");
    }
}
