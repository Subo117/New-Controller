using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.PlayerMovementActions playerMovement;
    PlayerMove playerMove;
    PlayerLook playerLook;


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerMovement = playerInput.PlayerMovement;
        playerMove = GetComponent<PlayerMove>();
        playerLook = GetComponent<PlayerLook>();
    }

    private void OnEnable()
    {
        playerMovement.Enable();
        playerMovement.Jump.performed += OnJump;
        playerMovement.Switch.performed += OnSwitch;
    }

    private void OnDisable()
    {
        playerMovement.Jump.performed -= OnJump;
        playerMovement.Switch.performed -= OnSwitch;
        playerMovement.Disable();
    }
    private void Update()
    {
        playerMove.ProcessMove(playerMovement.Move.ReadValue<Vector2>());
        playerMove.ProcessSprint(playerMovement.Sprint.IsPressed());
        if (!playerMove.isTPP)
        {
            playerLook.ProcessLook(playerMovement.Look.ReadValue<Vector2>());
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        playerMove.ProcessJump();
    }

    void OnSwitch(InputAction.CallbackContext context)
    {
        playerMove.ProcessSwitch();
    }
}
