using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the movement of the player with given input from the input manager
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The speed at which the player moves")]
    public float moveSpeed = 2f;
    [Tooltip("The speed at which the player rotates to look left and right (calculated in degrees)")]
    public float lookSpeed = 60f;
    [Tooltip("The power with which the player jumps")]
    public float jumpPower = 8f;
    [Tooltip("The strength of gravity")]
    public float gravity = 9.81f;

    [Header("Jump Timing")]
    [Tooltip("How long after the player has moved off of something they can jump off of they can still jump")]
    public float jumpTimeLeniency = 0.1f;
    //The time when the leniency ends
    float timeToStopBeingLenient = 0;

    // Whether or not the double jump is available
    bool doubleJumpAvailable = false;

    // the character controller component
    private CharacterController controller;

    // The input manager to read input from
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        SetUpCharacterController();
        SetUpInputManager();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }

    /// <summary>
    /// Description:
    /// Gets the attached character controller compoenent if one exists
    /// Input:
    /// none
    /// Return:
    /// void
    /// </summary>
    void SetUpCharacterController()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("The player controller script does not have a character controller on the same gameobject!");
        }
    }

    /// <summary>
    /// Description:
    /// Gets the input manager from the scene
    /// Input:
    /// none
    /// Return:
    /// void
    /// </summary>
    void SetUpInputManager()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    // The direction in which the controller is moving
    Vector3 moveDirection;
    void ProcessMovement()
    {
        // Get the input from the input manager
        float leftRightInput = inputManager.horizontalMoveAxis;
        float forwardBackwardInput = inputManager.verticalMoveAxis;
        bool jumpPressed = inputManager.jumpPressed;

        // The controller handles parts of the movement differently depending on the grounded state
        if (controller.isGrounded)
        {
            // Continually update the time to stop being lenient to allow us leway on jumping when exiting the ground
            timeToStopBeingLenient = Time.time + jumpTimeLeniency;
            // Make double jump available
            doubleJumpAvailable = true;

            // Set the movement direction to be the received input, set y to 0 since we are on the ground
            moveDirection = new Vector3(leftRightInput, 0, forwardBackwardInput);
            // Set the move direction in relation to the transform
            moveDirection = transform.TransformDirection(moveDirection);
            // multiply the movement by move speed
            moveDirection *= moveSpeed;

            // If jump was pressed apply the jump power to the y direction
            if (jumpPressed)
            {
                moveDirection.y = jumpPower;
            }

        }
        else
        {
            // Set the movement direction to be the received input multiplied by the move speed
            // Let y stay as is so gravity can take effect
            moveDirection = new Vector3(leftRightInput * moveSpeed, moveDirection.y, forwardBackwardInput * moveSpeed);
            // Set the move direction in relation to the transform
            moveDirection = transform.TransformDirection(moveDirection);

            // If jump is pressed and we are within our lenient time frame, apply a jump power
            // otherwise se if we are double jumping
            if (jumpPressed && Time.time < timeToStopBeingLenient)
            {
                moveDirection.y = jumpPower;
            }
            else if (jumpPressed && doubleJumpAvailable)
            {
                moveDirection.y = jumpPower;
                doubleJumpAvailable = false;
            }
        }

        // apply the force of gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // If we are grouded and our y movement is negative set it to be a small negative number
        // This keeps the controller grounded while preventing the downward force of gravity from piling up
        if (controller.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -0.3f;
        }

        // Apply the movement using the built in chracter controller move function
        controller.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Description:
    /// Bounces the player upwards
    /// Inputs: N/A
    /// Outputs: N/A
    /// </summary>
    public void Bounce()
    {
        Debug.Log("BOUNCE");
        if (inputManager.jumpHeld)
        {
            moveDirection.y = jumpPower * 1.5f;
        }
        else
        {
            moveDirection.y = jumpPower * 1f;
        }
    }

    /// <summary>
    /// Description:
    /// Process the horizontal look input to rotate the player accordingly
    /// Input:
    /// none
    /// Return:
    /// void
    /// </summary>
    void ProcessRotation()
    {
        float horizontalLookInput = inputManager.horizontalLookAxis;
        Vector3 playerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(playerRotation.x, playerRotation.y + horizontalLookInput * lookSpeed * Time.deltaTime, playerRotation.z));
    }
}
