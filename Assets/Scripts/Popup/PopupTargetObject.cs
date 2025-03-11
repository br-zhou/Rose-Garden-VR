// Example implementation of IEventReceiver
using TMPro;
using UnityEngine;

public class PopupchildObject : MonoBehaviour, IRayEventReceiver
{
    [SerializeField]
    private GameObject child;
    [SerializeField]
    private GameObject focusPoint;

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
        }
    }

    void Update()
    {
        child.transform.position = Vector3.Lerp(child.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void OnRaycastEnter()
    {
        Debug.Log("Popup Cast Enter");
        targetPosition = focusPoint.transform.position;

    }

    public void OnRaycastExit()
    {
    }
}
