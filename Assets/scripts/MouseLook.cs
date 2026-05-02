using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCamera : MonoBehaviour
{
    public Transform playerBody; // This MUST be assigned in the Inspector
    public float sensitivity = 220f;
    public float mouseSensitivity = 0.12f;
    public float minPitch = -70f;
    public float maxPitch = 70f;
    public bool mobileMode = true;

    private float yaw;
    private float pitch;
    private Vector2 lookInput;
    void Start()
    {
        if (playerBody != null)
        {
            yaw = playerBody.eulerAngles.y;
        }

        pitch = NormalizeAngle(transform.localEulerAngles.x);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        if (context.canceled) lookInput = Vector2.zero;
    }

    void Update()
    {
        if (playerBody == null) return;

        Vector2 lookDelta = GetLookDelta();
        yaw += lookDelta.x;
        pitch -= lookDelta.y;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // PUBG / Roblox style mobile look: drag on the right side turns the player,
        // while the camera only handles vertical pitch.
        playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private Vector2 GetLookDelta()
    {
        if (mobileMode)
        {
            // Use the existing on-screen right-stick / gamepad path for mobile mode.
            return lookInput * sensitivity * Time.deltaTime;
        }

        return lookInput * mouseSensitivity;
    }

    public void SetMobileMode(bool enabled)
    {
        mobileMode = enabled;
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }

        return angle;
    }
}
