using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCamera : MonoBehaviour
{
    public Transform playerBody; // This MUST be assigned in the Inspector
    public float sensitivity = 15f; 
    private float xRotation = 0f;
    private Vector2 lookInput;

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        if (context.canceled) lookInput = Vector2.zero;
    }

    void Update()
    {
        if (playerBody == null) return;

        float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime;

        // Vertical Look (Up/Down) - Rotates only the Camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal Look (Left/Right) - Rotates the entire Player
        playerBody.Rotate(Vector3.up * mouseX);
    }
}