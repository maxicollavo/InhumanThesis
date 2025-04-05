using System;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Camera Settings")]
    [SerializeField] private bool invertYAxis = false;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float clampRange = 80f;

    private CharacterController characterController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private Vector3 currentMovement = Vector3.zero;
    private float verticalRotation;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = PlayerInputHandler.Instance;

        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        float mouseYInput = invertYAxis ? -inputHandler.LookInput.y : inputHandler.LookInput.y;

        float mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= mouseYInput * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -clampRange, clampRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);
    }

    private void HandleMovement()
    {
        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * moveSpeed;
        currentMovement.z = worldDirection.z * moveSpeed;

        characterController.Move(currentMovement * Time.deltaTime);
    }
}