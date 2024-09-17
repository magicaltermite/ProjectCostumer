using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private float horizontalSensitivity = 10.0f;
    [SerializeField] private float verticalSensitivity = 10.0f;
    [SerializeField] private Vector2 rotationMinMax = new Vector2(-40, 40);
    
    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private float distanceFromTarget = 6.0f;
    [SerializeField] private float cameraHeight = -6.0f;

    [SerializeField] private float smoothTime = 0.2f;
    
    private float rotationX;
    private float rotationY;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    

    private Transform thisTransform;

    private void Start() {
        thisTransform = transform;
    }

    private void Update() {
        PositionCamera();
        RotateCamera();
    }


    private void RotateCamera() {
        float mouseX = Input.GetAxis("Mouse X") * verticalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * horizontalSensitivity;

        rotationY += mouseX;
        rotationX += mouseY;
        rotationX = Mathf.Clamp(rotationX, rotationMinMax.x, rotationMinMax.y);

        Vector3 nextRotation = new (rotationX, rotationY);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

        transform.localEulerAngles = currentRotation;
    }

    private void PositionCamera() {
        thisTransform.position = cameraTarget.transform.position - transform.forward * distanceFromTarget - transform.up * cameraHeight;
    }
}
