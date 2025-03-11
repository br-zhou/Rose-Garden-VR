using UnityEngine;

public class SmoothMoveTowardsCameraRig : MonoBehaviour, IRayEventReceiver
{
    public Transform cameraRig; // Reference to the Camera Rig
    public float moveSpeed = 2.0f; // Speed of the smooth movement
    public float distance = 5.0f; // Desired distance from the Camera Rig
    public float heightOffset = 2.0f; // Height offset from the Camera Rig
    private Vector3 targetPosition;
    private bool isMovingTowardsTarget = false;

    void Start()
    {
        // Ensure the cameraRig reference is set
        if (cameraRig == null)
        {
            Debug.LogError("Camera Rig is not assigned.");
            return;
        }

        // Calculate the initial target position
        targetPosition = cameraRig.position - cameraRig.forward * distance;
        targetPosition.y += heightOffset; // Apply the height offset
    }

    void Update()
    {
        if (isMovingTowardsTarget)
        {
            // Continuously update the target position based on the Camera Rig's position
            targetPosition = cameraRig.position - cameraRig.forward * distance;
            targetPosition.y += heightOffset; // Apply the height offset
            // Smoothly interpolate the position towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void OnRaycastEnter()
    {
        // Start moving towards the Camera Rig
        isMovingTowardsTarget = true;
    }

    // Only one definition of OnRaycastExit method
    public void OnRaycastExit()
    {
        // Stop moving
        isMovingTowardsTarget = false;
    }
}
