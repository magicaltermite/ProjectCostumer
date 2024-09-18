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
    [SerializeField] private Image[] SmallCluePanels;
    [SerializeField] private Image[] BigCluePanels;

    
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

    private void Start() {
        pickupPrompt.gameObject.SetActive(false);
    }

    public void EnablePickupPrompt(bool isEnabled) {
        pickupPrompt.gameObject.SetActive(isEnabled);
    }

    public void EnableDialogueBox(bool isEnabled, string file) {
        dialogueBox.gameObject.SetActive(isEnabled);
        DialogueController.Instance.SetDialogueFile(file);
    }

    public void UpdateClueUI(ClueTypes clueType) {
        Image currentBigCluePanel = null;
        Image currentSmallCluePanel = null;
        
        for (int i = 0; i < BigCluePanels.Length; i++) {
            if (BigCluePanels[i].name.Equals(clueType + "Panel")) {
                currentBigCluePanel = BigCluePanels[i];
            }
            if (SmallCluePanels[i].name.Equals(clueType + "Panel")) {
                currentSmallCluePanel = SmallCluePanels[i];
            }
        }

        if (currentBigCluePanel is null || currentSmallCluePanel is null)
            return;
        
        switch (clueType) {
            case ClueTypes.Campfire:
                currentBigCluePanel.GetComponent<Image>().color = Color.red;
                currentSmallCluePanel.GetComponent<Image>().color = Color.red;
                break;
            case ClueTypes.BurntLeaves:
                currentBigCluePanel.GetComponent<Image>().color = Color.blue;
                currentSmallCluePanel.GetComponent<Image>().color = Color.blue;
                break;
            case ClueTypes.PineCone:
                currentBigCluePanel.GetComponent<Image>().color = Color.green;
                currentSmallCluePanel.GetComponent<Image>().color = Color.green;
                break;
            case ClueTypes.VShapedBurnMark:
                currentBigCluePanel.GetComponent<Image>().color = Color.yellow;
                currentSmallCluePanel.GetComponent<Image>().color = Color.yellow;
                break;
            default:
                throw new Exception("Clue type does not exist");
        }
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
