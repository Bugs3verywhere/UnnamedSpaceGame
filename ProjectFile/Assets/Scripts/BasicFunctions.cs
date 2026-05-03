using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Speeds")]
    public float walkSpeed = 3.5f;
    public float runSpeed = 6f;
    public float crouchSpeed = 2f;

    [Header("Jumping")]
    public float jumpPower = 6.5f;
    public float gravity = 20f;

    [Header("Looking Around")]
    public float lookSpeed = 2000f;
    public float lookXLimit = 45f;

    [Header("Heights")]
    public float standingHeight = 4f;
    public float crouchHeight = 2f;

    [Header("Sprint Settings")]
    public float maxStamina = 50f;
    public float sprintDrainRate = 10f;
    private float staminaRegenRate = 5f;
    public float regenDelay = 2f;
    private float stamina;
    private bool isSprinting = false;


    [Header("UI")]
    public TMP_Text staminaText;

    [Header("Head Bob")]
    public float walkBobSpeed = 10f;
    public float sprintBobSpeed = 16f;
    public float crouchBobSpeed = 7f;

    public float walkBobAmount = 0.04f;
    public float sprintBobAmount = 0.07f;
    public float crouchBobAmount = 0.025f;

    private float bobTimer = 0f;
    private Vector3 cameraStartPos;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    void Start()

    {
        characterController = GetComponent<CharacterController>();
        stamina = maxStamina;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ==== INPUTS ====
        bool sprintInput = Keyboard.current.leftShiftKey.isPressed;
        bool isCrouching = Keyboard.current.leftCtrlKey.isPressed;

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        if (Keyboard.current.aKey.isPressed) inputX -= 1;
        if (Keyboard.current.dKey.isPressed) inputX += 1;
        if (Keyboard.current.sKey.isPressed) inputZ -= 1;
        if (Keyboard.current.wKey.isPressed) inputZ += 1;


        // ==== SPRINT LOGIC ====

        if (sprintInput && !isCrouching && (inputX != 0 || inputZ != 0) && stamina > 0)
        {
            isSprinting = true;
            stamina -= sprintDrainRate * Time.deltaTime;
            if (stamina < 0) stamina = 0;
        }
        else
        {
            isSprinting = false;
        }

        stamina += staminaRegenRate * Time.deltaTime;
        if (stamina > maxStamina) stamina = maxStamina;
        {

        }


        // ==== SPEED ====
        float currentSpeed = walkSpeed;

        if (isCrouching)
            currentSpeed = crouchSpeed;
        else if (isSprinting)
            currentSpeed = runSpeed;


        // ==== MOVEMENT ====
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float curSpeedX = canMove ? currentSpeed * inputZ : 0;
        float curSpeedY = canMove ? currentSpeed * inputX : 0;

        Vector3 horizontalMovement = (forward * curSpeedX) + (right * curSpeedY);

        if (characterController.isGrounded)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && canMove)
                moveDirection.y = jumpPower;
            else if (moveDirection.y < 0)
                moveDirection.y = -2f;
        }


        moveDirection.y -= gravity * Time.deltaTime;


        Vector3 finalMove = horizontalMovement + Vector3.up * moveDirection.y;

        characterController.Move(finalMove * Time.deltaTime);

        characterController.height = isCrouching ? crouchHeight : standingHeight;


        // ==== LOOKING AROUND ====
        if (canMove && Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            rotationX += -mouseDelta.y * lookSpeed * 0.1f;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseDelta.x * lookSpeed * 0.1f);
        }

        
        // ==== HEAD BOB ====
        HandleHeadBob(inputX, inputZ, isCrouching);
    }

    void HandleHeadBob(float inputX, float inputZ, bool isCrouching)
    {
        if (!characterController.isGrounded || (inputX == 0 && inputZ == 0))
        {
            playerCamera.transform.localPosition = 
            Vector3.Lerp(playerCamera.transform.localPosition, cameraStartPos, Time.deltaTime * 5f);

            return;
        }

        float bobSpeed = walkBobSpeed;
        float bobAmount = walkBobAmount;

        if (isSprinting)
        {
            bobSpeed = sprintBobSpeed;
            bobAmount = sprintBobAmount;
        }

        else if (isCrouching)
        {
            bobSpeed = crouchBobSpeed;
            bobAmount = crouchBobAmount;
        }

        bobTimer += Time.deltaTime * bobSpeed;

        Vector3 bobPosition = cameraStartPos;
        bobPosition.y += Mathf.Sin(bobTimer) * bobAmount;
        bobPosition.x += Mathf.Cos(bobTimer / 2) * (bobAmount * 0.5f);

        playerCamera.transform.localPosition = bobPosition;

    }

        // ==== UI DISPLAY ====
    private void OnGUI()
    {
        //Stamina
        GUIStyle staminaStyle = new GUIStyle(GUI.skin.label);
        staminaStyle.fontSize = 15;
        staminaStyle.normal.textColor = Color.blue;

        GUI.Label(new Rect(20, 20, 300, 40), "Stamina: " + stamina.ToString("F1"), staminaStyle);

    }
}

