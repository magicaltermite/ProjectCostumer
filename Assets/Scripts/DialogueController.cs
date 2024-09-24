using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour {
    public static DialogueController Instance { get; private set; }
    public bool ChoiceDialogueDone { get; private set; } // this is an ugly solution for making the choice work
    public bool DialogueIsRunning { get; private set; }
    
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private float textSpeed;

    private int index;
    private string[] lines;
    private string filePath;
    private bool hasStarted; // This is a clunky solution, but I'm having some problems with the dialogue working correctly more than once

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void Start() {
        textBox.text = string.Empty;
        StartDialogue();
        hasStarted = true;
        DialogueIsRunning = true;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (textBox.text == lines[index]) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textBox.text = lines[index];
            }
        }
    }

    private void StartDialogue() {
        string dialogueContents;
        
        try {
            using StreamReader sr = new StreamReader(filePath);
            dialogueContents = sr.ReadToEnd();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }

        DialogueWrapper dialogueWrapper = new(dialogueContents);
        dialogueWrapper.PopulateArray();
        
        if (textBox.isActiveAndEnabled) {
            Instance.PopulateLines(dialogueWrapper.DialogueArray);
        }
        
        index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine() {
        foreach (char c in lines[index].ToCharArray()) {
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            textBox.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else {
            textBox.text = string.Empty;
            DialogueIsRunning = false; // this is for allowing objects to react to the dialogue ending, such as the buttons in the choice scene
            
            if (SceneManager.GetActiveScene().name.Equals("new_level_1")) {
                GameManager.Instance.PutOwlOnHead();
            }
            
            gameObject.SetActive(false);
            GameManager.Instance.IncrementClueCounter();
            
            if (ChoiceDialogueDone) {
                // This is part of making the choice scene load the correct scene after the dialogue is done 
                GameManager.Instance.LoadScene("TestEndScene");
            }
        }
    }


    private void PopulateLines(string[] linesToShow) {
        lines = linesToShow;
    }

    public void SetDialogueFile(string file) {
        filePath = Application.streamingAssetsPath + file;
        if(hasStarted)
            StartDialogue();
        // this is an ugly solution for ensuring that the dialogue is done, but I think I will have to rewrite large portions of the dialogue script otherwise
        if (file.Equals("/Dialogue/ChoiceDialogueCorrect.txt")) {
            ChoiceDialogueDone = true;
        }
    }
    
}
