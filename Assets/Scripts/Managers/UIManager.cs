using System;
using System.IO;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace Managers {
public class UIManager : MonoBehaviour {
    
    public static UIManager Instance { get; private set; }
    
    [Header("UI Elements")]
    [SerializeField] private TMP_Text pickupPrompt;
    [SerializeField] private Image dialogueBox;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] public GameObject smallCluesObject;
    [SerializeField] public GameObject bigCluesObject;
    
    [Header("Clue panels")]
    [SerializeField] private Image[] SmallCluePanels;
    [SerializeField] private Image[] BigCluePanels;
    [SerializeField] private Image[] pauseMenuPanels; 
    
    [Header("Clue Panel images")]
    [SerializeField] public Sprite campFound;
    [SerializeField] public Sprite cigFound;
    [SerializeField] public Sprite pineFound;
    [SerializeField] public Sprite leavesFound;

    [Header("Option menu slider")]
    [SerializeField] private Slider sensitivityXSlider;
    [SerializeField] private Slider sensitivityYSlider;
    [SerializeField] private Toggle invertX;
    [SerializeField] private Toggle invertY;
    
    [SerializeField] private CameraController cameraController;
    
    public bool isSmallActive;
    public bool pause = false;
    private GameObject canvas;

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            smallCluesObject.SetActive(isSmallActive);
            bigCluesObject.SetActive(!isSmallActive);
            isSmallActive = !isSmallActive;
        }
    }

    

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    public void PauseGame(bool isPaused) {
        pauseMenu.SetActive(isPaused);
        pause = isPaused;
        smallCluesObject.SetActive(!isPaused);
        GameManager.Instance.CaptureMouse(!isPaused);
    }

    private void Start() {
        pickupPrompt.gameObject.SetActive(false);
    }

    public void EnablePickupPrompt(bool isEnabled) {
        pickupPrompt.gameObject.SetActive(isEnabled);
    }

    public void EnableDialogueBox(bool isEnabled, bool isClue, string file) {
        dialogueBox.gameObject.SetActive(isEnabled);
        DialogueController.Instance.SetDialogueFile(file);
        DialogueController.Instance.IsThisAClue(isClue);
    }

    public void UpdateClueUI(ClueTypes clueType) {
        Image currentBigCluePanel = null;
        Image currentSmallCluePanel = null;
        Image currentPauseMenuPanel = null;
        
        for (int i = 0; i < BigCluePanels.Length; i++) {
            if (BigCluePanels[i].name.Equals(clueType + "Panel")) {
                currentBigCluePanel = BigCluePanels[i];
            }
            if (SmallCluePanels[i].name.Equals(clueType + "Panel")) {
                currentSmallCluePanel = SmallCluePanels[i];
            }
            if (pauseMenuPanels[i].name.Equals(clueType + "Panel")) {
                currentPauseMenuPanel = pauseMenuPanels[i];
            }
        }

        if (currentBigCluePanel is null || currentSmallCluePanel is null || currentPauseMenuPanel is null) return;
        
        switch (clueType) {
            case ClueTypes.Campfire:
                currentBigCluePanel.GetComponent<Image>().sprite = campFound;
                currentSmallCluePanel.GetComponent<Image>().sprite = campFound;
                currentPauseMenuPanel.GetComponent<Image>().sprite = campFound;
                break;
            case ClueTypes.BurntLeaves:
                currentBigCluePanel.GetComponent<Image>().sprite = leavesFound;
                currentSmallCluePanel.GetComponent<Image>().sprite = leavesFound;
                currentPauseMenuPanel.GetComponent <Image>().sprite = leavesFound;
                break;
            case ClueTypes.PineCone:
                currentBigCluePanel.GetComponent<Image>().sprite = pineFound;
                currentSmallCluePanel.GetComponent<Image>().sprite = pineFound;
                currentPauseMenuPanel.GetComponent<Image>().sprite = pineFound;
                break;
            case ClueTypes.Cigarette:
                currentBigCluePanel.GetComponent<Image>().sprite = cigFound;
                currentSmallCluePanel.GetComponent<Image>().sprite = cigFound;
                currentPauseMenuPanel.GetComponent<Image>().sprite = cigFound;
                break;
            default:
                throw new Exception("Clue type does not exist");
        }

    }

    public void ChangeSensitivity() {
        cameraController.SetCameraSensitivity(sensitivityXSlider.value, sensitivityYSlider.value);
    }

    public void InvertCamera() {
        cameraController.SetInvertedCamera(invertX.isOn, invertY.isOn);
    }
    
}
}

public class DialogueWrapper {
    // object used to handle the dialogue from the text files and turn it into an array
    private string DialogueContents { get; }
    public string[] DialogueArray { get; private set; }

    public DialogueWrapper(string dialogueContents) {
        DialogueContents = dialogueContents;
    }

    public void PopulateArray() {
        DialogueArray = DialogueContents.Split(";");
    }
}
