using UnityEngine;

public class MoveRotateScale : MonoBehaviour
{
    public Vector3 moveDistance = new Vector3(5.0f, 5.0f, 5.0f); // Distance to move on the xyz axes
    public float rotationAngle = 360.0f; // Rotation angle
    public float scaleMultiplier = 3.0f; // Scale factor
    public float moveSpeed = 2.0f; // Speed of movement and scaling
    public float rotationSpeed = 1.0f; // Initial rotation speed
    public float rotationAcceleration = 0.1f; // Acceleration for rotation

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isMovingToTarget = false;
    private bool isRotating = false;
    private float currentRotationSpeed;

    void Start()
    {
        // Store the original position, rotation, and scale of the object
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;

        // Calculate the target position and scale
        targetPosition = originalPosition + moveDistance;
        targetScale = originalScale * scaleMultiplier;
        currentRotationSpeed = rotationSpeed;
    }

    void Update()
    {
        // Check if the "2" key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isMovingToTarget = !isMovingToTarget; // Toggle the movement state
        }

        // Move and scale smoothly based on the toggle state
        if (isMovingToTarget)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, moveSpeed * Time.deltaTime);
        }

        // Check if the "3" key is pressed
        if (Input.GetKey(KeyCode.Alpha3))
        {
            isRotating = true;
            currentRotationSpeed += rotationAcceleration * Time.deltaTime; // Accelerate rotation
        }
        else
        {
            isRotating = false;
            currentRotationSpeed = rotationSpeed; // Reset to initial rotation speed
        }

        // Rotate the object smoothly
        if (isRotating)
        {
            transform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, currentRotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward, currentRotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
