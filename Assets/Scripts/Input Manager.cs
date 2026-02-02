using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.PlayerMovementActions playerMovement;
    PlayerMove playerMove;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerMovement = playerInput.PlayerMovement;
        playerMove = GetComponent<PlayerMove>();
    }

    private void OnEnable()
    {
        playerMovement.Enable();
        playerMovement.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        playerMovement.Jump.performed -= OnJump;
        playerMovement.Disable();
    }
    private void Update()
    {
        playerMove.ProcessMove(playerMovement.Move.ReadValue<Vector2>());
        playerMove.ProcessSprint(playerMovement.Sprint.IsPressed());
    }

    void OnJump(InputAction.CallbackContext context)
    {
        playerMove.ProcessJump();
    }
}
