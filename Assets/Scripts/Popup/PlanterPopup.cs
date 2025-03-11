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
    private Animator flowerAnimator;

    private PlanterController childController;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    private Vector3 targetPosition;

    private float moveSpeed = 2.0f;

    void Start()
    {
        if (child)
        {
            initialPosition = child.transform.position;
            initialRotation = child.transform.rotation;
            initialScale = child.transform.localScale;

            targetPosition = initialPosition;
            childController = child.GetComponent<PlanterController>();
        }
    }

    void Update()
    {
        child.transform.position = Vector3.Lerp(child.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void OnRaycastEnter()
    {
        OpenPopup();
    }

    public void OnRaycastExit() { }

    public void OpenPopup() {
        targetPosition = focusPoint.transform.position;
        GameManager.Instance.setCurrentPopup(GetComponent<PopupEvent>());
        childController.setIsActive(true);
    }

    public void ClosePopup() {
        targetPosition = initialPosition;
        childController.setIsActive(false);
    }
}
