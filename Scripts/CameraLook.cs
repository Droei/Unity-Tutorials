using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 20f;

    float xRotation;

    PlayerInputActions inputActions;
    InputAction lookAction;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        lookAction = inputActions.Player.Look;
    }

    private void OnEnable() => lookAction.Enable();

    private void OnDisable() => lookAction.Disable();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 look = lookAction.ReadValue<Vector2>();

        float mouseX = look.x * sensitivity * Time.deltaTime;
        float mouseY = look.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.parent.Rotate(Vector3.up * mouseX);
    }

}
