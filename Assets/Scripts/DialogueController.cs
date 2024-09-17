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
        string path = Application.streamingAssetsPath + "/Dialogue/TestDialogue.txt";
        
        try {
            using StreamReader sr = new StreamReader(path);
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
            gameObject.SetActive(false);
        }
    }


    public void PopulateLines(string[] linesToShow) {
        lines = linesToShow;
    }
    
}
