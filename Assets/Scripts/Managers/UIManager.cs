using System;
using TMPro;
using UnityEngine;

namespace Managers {
public class UIManager : MonoBehaviour {
    
    [SerializeField] private TMP_Text pickupPrompt;
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
        Debug.Log("It is still not enabling ;-;: " + isEnabled);
        pickupPrompt.gameObject.SetActive(isEnabled);
    }
    
    
}
}
