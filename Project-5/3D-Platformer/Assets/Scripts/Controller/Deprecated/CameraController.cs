using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

/// <summary>
/// This class uses processed input from the input manager to control the vertical rotation of the camera
/// </summary>
/// [RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The speed at which the camera rotates to look up and down (calculated in degrees)")]
    public float rotationSpeed = 60f;
    [Tooltip("Whether or not to invert the look direction")]
    public bool invert = true;

    // The input manager to read input from
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        SetUpInputManager();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
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

    /// <summary>
    /// Description:
    /// Process the vertical look input to rotate the player accordingly
    /// Input:
    /// none
    /// Return:
    /// void
    /// </summary>
    void ProcessRotation()
    {
        float verticalLookInput = inputManager.verticalLookAxis;
        Vector3 cameraRotation = transform.rotation.eulerAngles;
        float newXRotation = 0;
        if (invert)
        {
            newXRotation  = cameraRotation.x - verticalLookInput * rotationSpeed * Time.deltaTime;
        }
        else
        {
            newXRotation = cameraRotation.x + verticalLookInput * rotationSpeed * Time.deltaTime;
        }

        // clamp the rotation 360 - 270 is up 0 - 90 is down
        // Because of the way eular angles work with Unity's rotations we have to act differently when clamping the rotation
        if (newXRotation < 270 && newXRotation >= 180)
        {
            newXRotation = 270;
        }
        else if (newXRotation > 90 && newXRotation < 180)
        {
            newXRotation = 90;
        }
        transform.rotation = Quaternion.Euler(new Vector3(newXRotation, cameraRotation.y, cameraRotation.z));

    }
}
