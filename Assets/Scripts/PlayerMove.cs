using UnityEngine;
using Unity.Cinemachine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject tppCamera;
    [SerializeField] GameObject fppCamera;
    [SerializeField] float walkSpeed = 7;
    [SerializeField] float sprintSpeed = 15;
    [SerializeField] float turnSpeed = 100;
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpHeight = 5f;

    CharacterController controller;
    GameObject currentCamera;

    float speed;
    float yVelocity;

    public bool isTPP = true;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;

        currentCamera = tppCamera;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    public void ProcessMove(Vector2 input)
    {

        Vector3 moveDir;
        if (isTPP)
        {
            // Camera-relative movement (TPP)
            Vector3 camForward = currentCamera.transform.forward;
            Vector3 camRight = currentCamera.transform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            moveDir = camForward * input.y + camRight * input.x;

            // Rotate player towards movement direction
            if (moveDir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    turnSpeed * Time.deltaTime
                );
            }
        }
        else
        {
            // Player-relative movement (FPP)
            moveDir = transform.forward * input.y + transform.right * input.x;
        }

        moveDir = Vector3.ClampMagnitude(moveDir, 1f);

        // Gravity handling
        if (controller.isGrounded && yVelocity < 0)
            yVelocity = -2f;

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = moveDir * speed;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
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

    public void ProcessSwitch()
    {
        isTPP = !isTPP;
        tppCamera.SetActive(isTPP);
        fppCamera.SetActive(!isTPP);

        currentCamera = isTPP ? tppCamera : fppCamera;
    }

}
