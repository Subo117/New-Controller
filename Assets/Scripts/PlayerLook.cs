using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 10f;
    [SerializeField] Transform camera;
    float xRot;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x * sensitivity * Time.deltaTime;
        float mouseY = input.y * sensitivity * Time.deltaTime;

        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, -70f, 70f);


        camera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(0f, mouseX, 0f);

    }

}
