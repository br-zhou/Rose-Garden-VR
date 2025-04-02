using UnityEngine;
using System.Collections;

public class PlanterScript : MonoBehaviour
{
    [Header("Swirling Movement Settings")]
    public float moveDuration = 3f;
    public float swirlAmplitude = 0.2f;
    public float swirlFrequency = 0.8f;

    [Header("Floating Movement Settings")]
    public float floatAmplitude = 0.1f;
    public float floatFrequency = 1.4f;
    public float rotationSpeed = 5f;

    [Header("Positions & Rotations")]
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    // The target position/rotation is set as the initial position/rotation in the Unity Editor
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    internal bool isAtTargetPosition = false; // whether the planter is at the target position
    internal bool isMoving = false; // whether the planter is moving between the original and target positions

    void Start()
    {
        // Save the original rotation and
        // reset the object to the original position
        // originalRotation = transform.rotation;
        targetRotation = transform.rotation;
        targetPosition = transform.position;
        transform.position = originalPosition;
    }

    void Update()
    {
        // While at the target position, make the planter float up and down
        if (isAtTargetPosition)
        {
            float newY = targetPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(targetPosition.x, newY, targetPosition.z);
            // transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // Move the planter to the target position
    internal void MovePlanterToTarget()
    {
        if (!isMoving && !isAtTargetPosition)
        {
            StartCoroutine(MovePlanterCoroutine("toTarget"));
        }
    }

    // Move the planter to the original position
    internal void MovePlanterToOriginal()
    {
        if (!isMoving && isAtTargetPosition)
        {
            isAtTargetPosition = false;
            StartCoroutine(MovePlanterCoroutine("toOriginal"));
        }
    }

    // Move the planter smoothly from startPos to endPos with the swirling motion
    IEnumerator MovePlanterCoroutine(string type)
    {
        Vector3 startPos, endPos;
        Quaternion startRot, endRot;
        startPos = transform.position;
        startRot = transform.rotation;
    
        if (type == "toTarget")
        {
            endPos = targetPosition;
            endRot = targetRotation;
        }
        else
        {
            endPos = originalPosition;
            endRot = originalRotation;
        }
        isMoving = true;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // Set the base position
            Vector3 basePos = Vector3.Lerp(startPos, endPos, smoothT);

            // Add a decreasing swirling offset
            float angle = smoothT * swirlFrequency * Mathf.PI * 2;
            float swirlMultiplier = 1 - 4 * Mathf.Pow(smoothT - 0.5f, 2);
            Vector3 swirlOffset = new Vector3(
                0.5f * Mathf.Cos(angle),
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            ) * swirlAmplitude * swirlMultiplier;

            // Add an increasing vertical offset (to connect to the floating motion)
            float offsetMultiplier;
            if (type == "toTarget")
                offsetMultiplier = smoothT;
            else
                offsetMultiplier = 1 - smoothT;

            Vector3 verticalOffset = new Vector3(
                0f,
                Mathf.Sin(Time.time * floatFrequency),
                0f
            ) * floatAmplitude * smoothT * offsetMultiplier;

            // Update the position
            transform.position = basePos + swirlOffset + verticalOffset;

            // Rotate the planter smoothly
            transform.rotation = Quaternion.Slerp(startRot, endRot, smoothT);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Set the final position and rotation
        transform.position = endPos;
        transform.rotation = endRot;
        if (type == "toTarget")
            isAtTargetPosition = true;
        isMoving = false;
    }
}
