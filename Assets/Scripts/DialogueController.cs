using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    public static DialogueController Instance { get; private set; }
    
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
            gameObject.SetActive(false);
        }
    }


    private void PopulateLines(string[] linesToShow) {
        lines = linesToShow;
    }

    public void SetDialogueFile(string file) {
        filePath = Application.streamingAssetsPath + "/Dialogue/" + file + ".txt";
        if(hasStarted)
            StartDialogue();
    }
    
}
