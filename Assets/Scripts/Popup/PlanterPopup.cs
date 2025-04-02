// Example implementation of IEventReceiver
using TMPro;
using UnityEngine;

public class PlanterPopup : MonoBehaviour, IRayEventReceiver, PopupEvent
{
    [SerializeField]
    private GameObject child;
    [SerializeField]
    private GameObject focusPoint;

    [SerializeField]
    private PlanterController childController;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isActive = true;
    private float moveSpeed = 2.0f;

    void Start()
    {
        if (child)
        {
            initialPosition = child.transform.position;
            initialRotation = child.transform.rotation;
            initialScale = child.transform.localScale;

            targetPosition = initialPosition;
            targetRotation = initialRotation;
        }
    }

    void Update()
    {
        child.transform.position = Vector3.Lerp(child.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        child.transform.rotation = Quaternion.Lerp(child.transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
    }

    public void OnRaycastExit() { }

    public void OpenPopup() {
        targetPosition = focusPoint.transform.position;
        targetRotation = focusPoint.transform.rotation;
        GameManager.Instance.setCurrentRayAction(this);
        Invoke("ActivateChildController", 2f); // triggers function after 1 second. stops notes animation from triggering immediately.
    }

    private void ActivateChildController()
    {
        if (!childController) return;
        childController.setIsActive(true);
    }
    private void DisableChildController()
    {
        if (!childController) return;
        childController.setIsActive(false);
    }

    public void ClosePopup() {
        targetPosition = initialPosition;
        targetRotation = initialRotation;
        //childController.setIsActive(false);
        DisableChildController();
    }

    public void OnRaycastEnter()
    {

    }

    public void Activate()
    {
        if (GameManager.Instance.isFocused()) return;
        OpenPopup();
        isActive = false;
    }

    public void DeActivate()
    {
        ClosePopup();
        isActive = true;
    }

    public bool CanReceiveRays()
    {
        return isActive;
    }
}
