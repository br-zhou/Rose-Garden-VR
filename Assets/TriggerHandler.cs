using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private bool isLeftClicked = false;
    private bool isRightClicked = false;
    private float clickTime = 0;

    [SerializeField]
    RayCaster raycaster;

    [SerializeField]
    OVRControllerMovement movementEdit;

    bool isEnabled = true;

    void Update()
    {
        if (!isEnabled)
        {
            if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Four))
            {
                print("PLAY MODE");
                isEnabled = true;
                movementEdit.isEnabled = false;
            }
            return;
        }

        bool leftPressed = IsAnyButtonPressed(OVRInput.Controller.LTouch);
        bool rightPressed = IsAnyButtonPressed(OVRInput.Controller.RTouch);
        

        if (rightPressed)
        {
            if (!isRightClicked)
            {
                HandleRightClick();
                isRightClicked = true;
                clickTime = Time.time;
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
                clickTime = Time.time;
            }
        }
        else
        {
            isLeftClicked = false;
        }


        if (leftPressed && rightPressed && (isRightClicked && isLeftClicked) && Time.time - clickTime > 1f && OVRInput.Get(OVRInput.Button.PrimaryThumbstick) && OVRInput.Get(OVRInput.Button.Two))
        {
            print("EDIT MODE");
            isEnabled = false;
            movementEdit.isEnabled = true;
        }

        if (leftPressed && rightPressed && (isRightClicked && isLeftClicked) && Time.time - clickTime > 2f)
        {
            GameManager.Instance.ResetInstructions();
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
        AudioManager.Instance.Play("unclick");
        GameManager.Instance.GoBack();
    }

    void HandleRightClick()
    {
        AudioManager.Instance.Play("click");
        raycaster.Activate();
    }
}