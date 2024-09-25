using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ChoiceSceneManager : MonoBehaviour {
    [SerializeField] private GameObject choiceButtons;

    private void Update() {
        if (!DialogueController.Instance.DialogueIsRunning) {
            choiceButtons.gameObject.SetActive(true);
        }
        else {
            choiceButtons.gameObject.SetActive(false);
        }
    }
}
