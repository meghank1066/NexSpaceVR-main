using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform cameraTransform; // Assign XR Camera (usually under XR Rig)
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate horizontally (yaw)
        yRotation += mouseX;

        // Rotate vertically (pitch) and clamp to avoid flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation
        cameraTransform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
