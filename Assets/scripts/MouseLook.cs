using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public Transform playerBody; // Drag Player here in Inspector
    public float sensitivity = 200f; // Increased for better feel
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        // Ensure camera is a child
        if (playerBody != null)
        {
            transform.SetParent(playerBody, true);
        }
    }

    void Update()
    {
        if (playerBody == null) return;

        // Get Input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Vertical Look (Camera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal Look (Player Body)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
