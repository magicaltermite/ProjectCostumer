using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float movementSpeed = 0.05f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float jumpHeight = 5f;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject raycastOrigin;
    
    private Vector3 playerVelocity;
    private CharacterController charController;

    private const float Gravity = -9.81f;
    

    // Start is called before the first frame update
    void Start() {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        MovePlayer();
    }


    private void MovePlayer() {
        if (GroundCheck() && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }
        
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        
        Vector3 movementDirection = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y ,0) *  new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        charController.Move(movementDirection * (movementSpeed * Time.deltaTime));
        
        if (movementDirection != Vector3.zero) {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }
        
        if (Input.GetButtonDown("Jump") && GroundCheck()) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Gravity);
        }

        playerVelocity.y += Gravity * Time.deltaTime;
        charController.Move(playerVelocity * Time.deltaTime);
    }

    private bool GroundCheck() {
        Transform originTransform = raycastOrigin.transform;
        return Physics.Raycast(originTransform.position, -originTransform.up, 0.05f);
    }
}