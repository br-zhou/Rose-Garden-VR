using UnityEngine;

public class SmoothMoveOnRayEvent : MonoBehaviour, IRayEventReceiver
{
    public float moveDistance = 5.0f; // Distance to move on the x-axis
    public float moveSpeed = 2.0f;    // Speed of the smooth movement
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMovingTowardsTarget = false;

    void Start()
    {
        // Store the original position of the object
        originalPosition = transform.position;
        // Calculate the target position
        targetPosition = originalPosition + new Vector3(moveDistance, 0, 0);
    }

    void Update()
    {
        // Smoothly interpolate the position
        if (isMovingTowardsTarget)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void OnRaycastEnter()
    {
        // Move towards the target position
        isMovingTowardsTarget = true;
    }

    public void OnRaycastExit()
    {
        // Move back to the original position
        isMovingTowardsTarget = false;
    }
}
