using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private bool isLeftClicked = false;
    private bool isRightClicked = false;

    [SerializeField]
    RayCaster raycaster;

    void Update()
    {
        bool leftPressed = IsAnyButtonPressed(OVRInput.Controller.LTouch);
        bool rightPressed = IsAnyButtonPressed(OVRInput.Controller.RTouch);

        if (rightPressed)
        {
            if (!isRightClicked)
            {
                HandleRightClick();
                isRightClicked = true;
            }
        }
        else
        {
            isRightClicked = false;
        }

        if (leftPressed)
        {
            if (!isLeftClicked)
            {
                HandleLeftClick();
                isLeftClicked = true;
            }
        }
        else
        {
            isLeftClicked = false;
        }
    }

    bool IsAnyButtonPressed(OVRInput.Controller controller)
    {
        return
            OVRInput.Get(OVRInput.Button.One, controller) ||
            OVRInput.Get(OVRInput.Button.Two, controller) ||
            OVRInput.Get(OVRInput.Button.Three, controller) ||
            OVRInput.Get(OVRInput.Button.Four, controller) ||
            OVRInput.Get(OVRInput.Button.PrimaryThumbstick, controller) ||
            OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, controller) ||
            OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, controller) ||
            OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, controller) ||
            OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, controller) ||
            OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller) ||
            OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller);
    }

    void HandleLeftClick()
    {
        GameManager.Instance.GoBack();
    }

    void HandleRightClick()
    {
        raycaster.Activate();
    }
}