using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Player stats")]
    [SerializeField] private float movementSpeed = 0.05f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float jumpHeight = 5f;
    
    [Header("Raycast information")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject raycastOrigin; // used for checking if the player is on the ground
    private readonly float raycastRadius = 3f;
    private readonly float raycastDistance = 0.5f;
    
    private Vector3 playerVelocity;
    private CharacterController charController;
    private Animator animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");


    private const float Gravity = -9.81f;
    

    // Start is called before the first frame update
    void Start() {
        charController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!GameManager.Instance.isPaused) {
            MovePlayer();
        }
        
    }


    private void MovePlayer() {
        if (GroundCheck() && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
            animator.SetBool(IsJumping, false);
        }
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        
        Vector3 movementDirection = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y ,0) *  new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        charController.Move(movementDirection * (movementSpeed * Time.deltaTime));
        
        if (movementDirection != Vector3.zero) {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            animator.SetBool(IsWalking, true);
        }
        else {
            animator.SetBool(IsWalking, false);
        }
        
        if (Input.GetButtonDown("Jump") && GroundCheck()) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Gravity);
            animator.SetBool(IsJumping, true);
        }
        
        playerVelocity.y += Gravity * 4 * Time.deltaTime;
        charController.Move(playerVelocity * Time.deltaTime);
    }

    private bool GroundCheck() {
        // Unity's build in character controller is really bad at checking if the ground is hit, so I made a custom one
        // Using a spherecast to make it a bit more lenient when the player can jump
        Transform originTransform = raycastOrigin.transform;
        RaycastHit hit;
        return Physics.SphereCast(originTransform.position, raycastRadius, -originTransform.up, out hit, raycastDistance);
    }

    // For debugging
    //private void OnDrawGizmos() {
    //    Transform originTransform = raycastOrigin.transform;
    //    Gizmos.DrawWireSphere(originTransform.position - originTransform.up * raycastDistance, raycastRadius);
    //}
}
