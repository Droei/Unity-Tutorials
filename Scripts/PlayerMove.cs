using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 2f;

    CharacterController controller;

    bool pressedJump = false;
    Vector3 velocity;
    Vector2 moveInput;

    private void Awake() => controller = GetComponent<CharacterController>();

    private void Update()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (controller.isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (pressedJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                pressedJump = false;
            }
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move((move * speed + velocity) * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pressedJump = true;
        }
    }
}

