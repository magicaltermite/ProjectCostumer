using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private float playerReach;
    
    private Interactable currentInteractable;
    private Camera mainCamera;

    private int layerMask;

    [SerializeField]
    public GameObject clue1;
    [SerializeField]
    public GameObject clue1Menu;

    [SerializeField]
    public GameObject smallPanels;
    [SerializeField]
    public GameObject menuPanels;
    public bool menuOpen = false; 

    private void Start() {
        mainCamera = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer("Interactable");
    }

    private void Update() {
        CheckInteraction();
        
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null) {
            currentInteractable.Interact();
            UIManager.Instance.UpdateClueUI(currentInteractable.GetClueType());
        }

        if (Input.GetKeyDown(KeyCode.Tab) && menuOpen == false) {
            Debug.Log("Tab pressed");
            menuPanels.SetActive(true);
            smallPanels.SetActive(false);
            menuOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && menuOpen == true) {
            smallPanels.SetActive(true);
            menuPanels.SetActive(false);
            menuOpen = false;
        }else if (GameManager.Instance.isPaused)
        {
            smallPanels.SetActive(false);
            menuPanels.SetActive(false);
        }
        else if(!GameManager.Instance.isPaused)
        {
            smallPanels.SetActive(true);
            menuPanels.SetActive(false);
        }
    }

    private void CheckInteraction() {
        Transform cameraTransform = mainCamera.transform;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, playerReach, layerMask)) {
            if (!hit.collider.CompareTag("Interactable")) return;
            
            Interactable newInteractable = hit.collider.GetComponent<Interactable>();

            if (newInteractable.enabled) {
                SetNewCurrentInteractable(newInteractable);
                UIManager.Instance.EnablePickupPrompt(true);
                Debug.Log("interaction occured");
            }
            else {
                DisableCurrentInteractable();
                UIManager.Instance.EnablePickupPrompt(false);

            }
        }
        else {
            DisableCurrentInteractable();
            UIManager.Instance.EnablePickupPrompt(false);
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable) {
        currentInteractable = newInteractable;
        currentInteractable.SetOutlineEnabled(true);
    }

    private void DisableCurrentInteractable() {
        if (!currentInteractable) return;
        
        currentInteractable.SetOutlineEnabled(false);
        currentInteractable = null;
    }
    
}

public enum ClueTypes {
    TestClue,
    Campfire,
    BurntLeaves,
    PineCone,
    VShapedBurnMark
    
}
