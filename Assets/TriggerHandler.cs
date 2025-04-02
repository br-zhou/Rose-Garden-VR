using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private bool isLeftClicked = false;
    private bool isRightClicked = false;

    [SerializeField]
    RayCaster raycaster;

    void Update()
    {
        float rightTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);

        float rightGrip = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);


        Debug.Log($"Right Trigger: {rightTrigger}, Left Trigger: {leftTrigger}");

        // Handle right trigger input
        if (rightTrigger > 0)
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

        // Handle left trigger input
        if (rightGrip > 0)
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

    void HandleLeftClick()
    {
        //raycaster.Activate();
        GameManager.Instance.GoBack();

    }

    void HandleRightClick()
    {
        //raycaster.DeActivate();
        raycaster.Activate();
        //GameManager.Instance.GoBack();
    }
}
