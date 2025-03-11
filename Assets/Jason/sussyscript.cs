using UnityEngine;

public class SwitchMoveRotateScale : MonoBehaviour
{
    public float moveDistance = 5.0f; // Distance to move on the x-axis
    public float rotationAngle = 17.0f; // Rotation angle on the Y-axis
    public float scaleMultiplier = 3.0f; // Scale factor
    public float moveSpeed = 2.0f; // Speed of movement and scaling

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isMovingToTarget = false;

    void Start()
    {
        // Store the original position, rotation, and scale of the object
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;

        // Calculate the target position, rotation, and scale
        targetPosition = originalPosition + new Vector3(moveDistance, 0, 0);
        targetRotation = originalRotation * Quaternion.Euler(0, rotationAngle, 0);
        targetScale = originalScale * scaleMultiplier;
    }

    void Update()
    {
        // Check if the "2" key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isMovingToTarget = !isMovingToTarget; // Toggle the movement state
        }

        // Move, rotate, and scale smoothly based on the toggle state
        if (isMovingToTarget)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, moveSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, moveSpeed * Time.deltaTime);
        }
    }
}
