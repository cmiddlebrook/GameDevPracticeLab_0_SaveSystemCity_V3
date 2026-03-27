using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour {


    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce;


    private CharacterController characterController;
    private float cameraVerticalAngle;
    private float characterVelocityY;


    private void Awake() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        HandleCharacterLook();
        HandleCharacterMovement();
    }

    private void HandleCharacterLook() {
        float lookX = Mouse.current.delta.x.value;
        float lookY = Mouse.current.delta.y.value;

        // Look horizontally
        transform.Rotate(new Vector3(0f, lookX * mouseSensitivity, 0f), Space.Self);

        // Look vertically
        cameraVerticalAngle -= lookY * mouseSensitivity;

        // Limit the camera's vertical angle to min/max
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        // Look vertically
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }

    private void HandleCharacterMovement() {
        float moveX = 0;
        float moveZ = 0;

        if (Keyboard.current.wKey.isPressed) {
            moveZ = +1f;
        }
        if (Keyboard.current.sKey.isPressed) {
            moveZ = -1f;
        }
        if (Keyboard.current.aKey.isPressed) {
            moveX = -1f;
        }
        if (Keyboard.current.dKey.isPressed) {
            moveX = +1f;
        }

        Vector3 characterVelocity = transform.right * moveX * moveSpeed + transform.forward * moveZ * moveSpeed;

        if (characterController.isGrounded) {
            characterVelocityY = 0f;
            // Jump
            if (IsInputJumpDown()) {
                characterVelocityY = jumpForce;
            }
        }

        characterVelocityY += gravityForce * Time.deltaTime;

        characterVelocity.y = characterVelocityY;

        characterController.Move(characterVelocity * Time.deltaTime);
    }

    private bool IsInputJumpDown() {
        return Keyboard.current.spaceKey.wasPressedThisFrame;
    }

    public void Move(Vector3 moveVector) {
        characterController.Move(moveVector * Time.deltaTime);
    }

}
