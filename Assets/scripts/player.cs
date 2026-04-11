using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    
    private Vector3 velocity;
    private Vector2 moveInput;

    void Start()
    {
        // This keeps your mouse visible and free
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnMove(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();
    public void OnJump(InputAction.CallbackContext context) { if (context.started) velocity.y = jumpForce; }

    void Update()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}