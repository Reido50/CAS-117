using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles reading the input given by the player through input devices
/// </summary>
public class InputManager : MonoBehaviour
{
    // A global reference for the input manager that outher scripts can access to read the input
    public static InputManager instance;

    /// <summary>
    /// Description:
    /// Standard Unity Function called when the script is loaded
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    private void Awake()
    {
        // Set up the instance of this
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Header("Player Movement Input")]
    [Tooltip("The move input along the horizontal")]
    public float horizontalMoveAxis;
    [Tooltip("The move input along the vertical")]
    public float verticalMoveAxis;
    public void ReadMovementInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalMoveAxis = inputVector.x;
        verticalMoveAxis = inputVector.y;
    }

    [Header("Look Around input")]
    public float horizontalLookAxis;
    public float verticalLookAxis;

    /// <summary>
    /// Description:
    /// Reads the movement input from the input actions's call back context.
    /// Input:
    /// InputAction.CallbackContext context
    /// Return:
    /// void
    /// </summary>
    /// <param name="context">The input action callback context meant to be read for movement</param>
    public void ReadMousePositionInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalLookAxis = inputVector.x;
        verticalLookAxis = inputVector.y;
    }

    [Header("Player Fire Input")]
    [Tooltip("Whether or not the fire button was pressed this frame")]
    public bool firePressed;
    [Tooltip("Whether or not the fire button is being held")]
    public bool fireHeld;

    /// <summary>
    /// Description:
    /// Reads the fire input from the input action's call back context
    /// Input:
    /// InputAction.CallbackContext context
    /// Returns:
    /// void
    /// </summary>
    /// <param name="context">The input action callback context meant to be read for firing</param>
    public void ReadFireInput(InputAction.CallbackContext context)
    {
        firePressed = !context.canceled;
        fireHeld = !context.canceled;
        StartCoroutine("ResetFireStart");
    }

    /// <summary>
    /// Description
    /// Coroutine that resets the fire pressed variable after one frame
    /// Inputs:
    /// none
    /// Returns: 
    /// IEnumerator
    /// </summary>
    /// <returns>IEnumerator: Allows this to function as a coroutine</returns>
    private IEnumerator ResetFireStart()
    {
        yield return new WaitForEndOfFrame();
        firePressed = false;
    }

    [Header("Pause Input")]
    public bool pausePressed;
    public void ReadPauseInput(InputAction.CallbackContext context)
    {
        pausePressed = !context.canceled;
        StartCoroutine(ResetPausePressed());
    }

    /// <summary>
    /// Description
    /// Coroutine that resets the pause pressed variable at the end of the frame
    /// Inputs:
    /// none
    /// Returns: 
    /// IEnumerator
    /// </summary>
    /// <returns>IEnumerator: Allows this to function as a coroutine</returns>
    IEnumerator ResetPausePressed()
    {
        yield return new WaitForEndOfFrame();
        pausePressed = false;
    }
}
