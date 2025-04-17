using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float crouchSpeed = 1.5f;

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

    private Vector3 originalCameraLocalPosition;
    private Vector3 crouchedCameraLocalPosition;

    private float originalHeight;
    private float crouchedHeight = 1.0f;
    private float currentSpeed;

    private float currentYPosition = 1.44f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float terminalVelocity = -50f;
    private float verticalVelocity = 0f;
    private bool isGrounded = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = PlayerInputHandler.Instance;
        originalCameraLocalPosition = mainCamera.transform.localPosition;
        crouchedCameraLocalPosition = new Vector3(originalCameraLocalPosition.x, originalCameraLocalPosition.y - 0.5f, originalCameraLocalPosition.z);

        originalHeight = characterController.height;
        currentSpeed = moveSpeed;

        characterController.center = Vector3.zero;
        mainCamera.transform.localPosition = originalCameraLocalPosition;

        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
    }

    private void Update()
    {
        if (!GameManager.Instance.canMove) return;

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
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void HandleMovement()
    {
        bool wantsToStand = !Keyboard.current.cKey.isPressed;
        bool ceilingAbove = IsCeilingAbove();
        bool shouldCrouch = Keyboard.current.cKey.isPressed || ceilingAbove;

        float targetYPosition = shouldCrouch ? 1f : 1.44f;
        currentYPosition = Mathf.Lerp(currentYPosition, targetYPosition, Time.deltaTime * 10f);

        float targetHeight = shouldCrouch ? crouchedHeight : originalHeight;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * 10f);

        Vector3 targetCamPos = shouldCrouch ? crouchedCameraLocalPosition : originalCameraLocalPosition;
        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, targetCamPos, Time.deltaTime * 10f);

        currentSpeed = shouldCrouch ? crouchSpeed : moveSpeed;

        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection).normalized;

        currentMovement.x = worldDirection.x * currentSpeed;
        currentMovement.z = worldDirection.z * currentSpeed;

        isGrounded = characterController.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
            verticalVelocity = Mathf.Max(verticalVelocity, terminalVelocity);
        }

        currentMovement.y = verticalVelocity;

        characterController.Move(currentMovement * Time.deltaTime);
    }

    private bool IsCeilingAbove()
    {
        // Punto de origen del raycast: desde el centro del jugador
        Vector3 origin = transform.position + Vector3.up * (characterController.height / 2f);

        // Distancia hasta donde vamos a chequear el techo
        float checkDistance = 0.6f;

        // Capa opcional si querés filtrar (usalo con LayerMask si querés)
        bool hasHit = Physics.Raycast(origin, Vector3.up, out RaycastHit hit, checkDistance, ~0, QueryTriggerInteraction.Ignore);

        // Debug visual
        Debug.DrawRay(origin, Vector3.up * checkDistance, hasHit ? Color.red : Color.green);

        return hasHit;
    }

}