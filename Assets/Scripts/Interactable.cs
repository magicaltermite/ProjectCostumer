using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour {
    [SerializeField] private string message;
    
    private Outline outline;
    public UnityEvent onInteraction;

    
    
    private void Start() {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        gameObject.tag = "Interactable";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact() {
        onInteraction.Invoke();
    }

    public void SetOutlineEnabled(bool isEnabled) {
        outline.enabled = isEnabled;
    }
    
    
}
