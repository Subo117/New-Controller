using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject tppCamera;
    [SerializeField] float walkSpeed = 7;
    [SerializeField] float sprintSpeed = 15;
    [SerializeField] float turnSpeed = 100;
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpHeight = 5f;

    CharacterController controller;

    float speed;
    float yVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 camForward = tppCamera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = tppCamera.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = camForward * input.y + camRight * input.x;

        Vector3 horizontalMove = moveDir;
        horizontalMove.y = 0;
        horizontalMove = Vector3.ClampMagnitude(horizontalMove, 1f);

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -2f;
        }
        yVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = horizontalMove * speed;
        finalMove.y = yVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }

    public void ProcessSprint(bool isSprinting)
    {
        speed = isSprinting ? sprintSpeed : walkSpeed;
    }

    public void ProcessJump()
    {
        if (controller.isGrounded)
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }
    }

}
