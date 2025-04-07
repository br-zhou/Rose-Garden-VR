using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private bool isLeftClicked = false;
    private bool isRightClicked = false;

    [SerializeField]
    RayCaster raycaster;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLeftClicked)
        {
            HandleLeftClick();
            isLeftClicked = true;
        }

        if (Input.GetMouseButtonDown(1) && !isRightClicked)
        {
            HandleRightClick();
            isRightClicked = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isLeftClicked = false;
        }

        if (Input.GetMouseButtonUp(1))
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
