using UnityEngine;
using UnityEngine.InputSystem;

public class ControlModeToggle : MonoBehaviour
{
    public GameObject mobileUI;
    public GameObject crosshair;
    public GameObject attackButton;
    public GameObject jumpButton;
    public GameObject respawnButton;
    public GameObject joystickLook;
    public GameObject joystickMove;
    public GameObject mobileIcon;
    public SimpleCamera cameraController;
    public bool mobileMode = true;

    private Kill respawnController;

    void Start()
    {
        respawnController = GetComponent<Kill>();
        ApplyMode();
    }

    void Update()
    {
        if (Keyboard.current?.hKey.wasPressedThisFrame ?? false)
        {
            mobileMode = !mobileMode;
            ApplyMode();
        }

        if (Keyboard.current?.rKey.wasPressedThisFrame ?? false)
        {
            respawnController?.Die();
        }
    }

    private void ApplyMode()
    {
        SetOptionalActive(mobileUI, true);
        SetOptionalActive(attackButton, mobileMode);
        SetOptionalActive(jumpButton, mobileMode);
        SetOptionalActive(respawnButton, mobileMode);
        SetOptionalActive(joystickLook, mobileMode);
        SetOptionalActive(joystickMove, mobileMode);
        SetOptionalActive(mobileIcon, mobileMode);

        if (crosshair != null)
        {
            crosshair.SetActive(true);
        }

        if (cameraController != null)
        {
            cameraController.SetMobileMode(mobileMode);
        }

        Cursor.lockState = mobileMode ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = mobileMode;
    }

    private void SetOptionalActive(GameObject target, bool isActive)
    {
        if (target != null)
        {
            target.SetActive(isActive);
        }
    }
}
