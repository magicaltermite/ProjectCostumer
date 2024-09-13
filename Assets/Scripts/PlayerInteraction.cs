using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private float playerReach;
    
    private Interactable currentInteractable;
    private Camera mainCamera;

    private int layerMask;
    

    private void Start() {
        mainCamera = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer("Interactable");
    }

    private void Update() {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null) {
            currentInteractable.Interact();
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
