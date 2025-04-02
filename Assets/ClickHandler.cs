using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private bool isLeftClicked = false;
    private bool isRightClicked = false;

    [SerializeField]
    RayCaster raycaster;

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            if (!isLeftClicked)
            {
                HandleLeftClick();
                isLeftClicked = true;
            }
        } else
        {
            isLeftClicked = false;
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (!isRightClicked)
            {
                HandleLeftClick();
                isRightClicked = true;
            }
        }
        else
        {
            isRightClicked = false;
        }
    }

    void HandleLeftClick()
    {
        raycaster.Activate();
    }

    void HandleRightClick()
    {
        //raycaster.DeActivate();

        GameManager.Instance.GoBack();
    }
}
