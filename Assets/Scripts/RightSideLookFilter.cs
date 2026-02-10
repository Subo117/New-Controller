using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class RightSideLookFilter : MonoBehaviour
{
    public InputActionReference lookAction;
    void Update()
    {
        if (Touchscreen.current == null)
            return;

        bool allowLook = false;

        foreach (var touch in Touchscreen.current.touches)
        {
            if (!touch.press.isPressed)
                continue;

            Debug.Log(touch.startPosition.ReadValue());
            float startX = touch.startPosition.ReadValue().x;

            if (startX > Screen.width * 0.5f)
            {
                allowLook = true;
                break;
            }
        }
        if (allowLook)
            lookAction.action.Enable();
        else
            lookAction.action.Disable();


    }
}
