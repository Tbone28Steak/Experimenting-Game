using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public CharacterController characterController;
    public GameObject playerCamera;

    [Header("Input Debug")]
    public Vector2 moveInput;
    public bool jumpInput;
    public Vector2 mouseInput;
    public bool sprintInput;

    [Header("Ability Control")]
    public Boolean canJump = true;
    public Boolean canSprint = true;
    public Boolean canMove = true;

    [Header("Settings")]
    public float sensitivity = 0.1f;
    public float moveSpeed = 3f;
    public float sprintSpeed = 6f;
    public float gravity = -12f;
    public float jumpPower = 10f;
    public float jumpLength = 0.15f;
    public float jumpCooldownLength = 0.3f;

    [Header("Debug")]
    public float speed;
    private float xRotation;
    private float yRotation;
    public bool isGrounded;
    public float time;
    public bool jumpCooldown;
    public float jumpTime;
    public bool jumping;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        jumpInput = playerInput.actions["Jump"].IsPressed();
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        sprintInput = playerInput.actions["Sprint"].IsPressed();
        mouseInput = playerInput.actions["Look"].ReadValue<Vector2>();

        MovementFunction();
        RotationManager();
    }

    void RotationManager()
    {
        xRotation -= mouseInput.y * sensitivity;
        yRotation += mouseInput.x * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    void MovementFunction() 
    {   float moveX = moveInput.x;
        float moveY = moveInput.y;

        if(sprintInput && canSprint) {speed = sprintSpeed;}
        else {speed = moveSpeed;}

        Vector3 movementDirection = transform.TransformDirection(new Vector3(moveX * speed, 0, moveY * speed));

        // on ground
        isGrounded = characterController.isGrounded;
        if(characterController.isGrounded) {
            movementDirection.y = -0.2f;
            time = 0;
        }

        // falling
        if(!characterController.isGrounded && !jumping) {
            movementDirection.y = Mathf.Lerp(-0.2f, gravity + gravity*time, time);
            time += 1f * Time.deltaTime;
        }

        // jumping
        if(jumpInput && !jumpCooldown && isGrounded && canJump) {
            jumping = true;
            jumpCooldown = true;
            jumpTime = 0;          
        }

        if(jumpCooldown) {jumpTime += 1f * Time.deltaTime;}
        if(jumping) {movementDirection.y = Mathf.Lerp(jumpPower, 0f, jumpTime);}

        if(jumpTime >= jumpLength) {jumping = false;}
        if(jumpTime >= jumpCooldownLength) {
            jumpCooldown = false;
            jumpTime = 0;
        }
        
        
        if(canMove) {
            characterController.Move(movementDirection * Time.deltaTime);
        }
        

        

    }

}
