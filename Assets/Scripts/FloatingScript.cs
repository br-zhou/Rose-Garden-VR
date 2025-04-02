using UnityEngine;
using System.Collections;

public class FloatingPlanter : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDuration = 3f;
    public float swirlAmplitude = 0.15f;
    public float swirlFrequency = 1.3f;

    [Header("Floating Settings")]
    public float floatAmplitude = 0.1f;
    public float floatFrequency = 1.4f;
    public float rotationSpeed = 5f;

    [Header("Positions & Rotations")]
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    
    // The target position is set in the editor (or can be set programmatically)
    private Vector3 targetPosition;
    private bool isAtTargetPosition = false; // are we at the target position?
    private bool isFloating = false; // are we in the floating animation?

    void Start()
    {
        // Save the original rotation and reposition the object to the original position.
        originalRotation = transform.rotation;
        targetPosition = transform.position;
        transform.position = originalPosition;
    }

    void Update()
    {
        // Press 8 to move from original to target with swirling motion.
        if (!isFloating && !isAtTargetPosition && Input.GetKeyDown(KeyCode.Alpha8))
        {
            StartCoroutine(Move(transform.position, targetPosition, () => {
                isAtTargetPosition = true;
            }));
        }

        // Press 7 to return to the original position.
        if (!isFloating && isAtTargetPosition && Input.GetKeyDown(KeyCode.Alpha7))
        {
            isAtTargetPosition = false;
            StartCoroutine(Move(transform.position, originalPosition, () =>
            {
                transform.rotation = originalRotation;
                transform.position = originalPosition;
            }));
        }

        // While in floating mode at the target, add a gentle vertical bob and continuous Y-axis rotation.
        if (isAtTargetPosition)
        {
            float newY = targetPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(targetPosition.x, newY, targetPosition.z);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // Generalized movement coroutine that can move from any start to any destination.
    IEnumerator Move(Vector3 startPos, Vector3 endPos, System.Action onComplete = null)
    {
        isFloating = true;
        Quaternion startRot = transform.rotation;
        // In this example, we keep the rotation of the destination equal to the object's original rotation.
        Quaternion endRot = originalRotation;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            // Smooth the progression
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // Base position (linear interpolation)
            Vector3 basePos = Vector3.Lerp(startPos, endPos, smoothT);

            // Swirling offset. We let the amplitude fade out as we approach the destination.
            float angle = smoothT * swirlFrequency * Mathf.PI * 2;
            float damping = 1 - smoothT;
            Vector3 swirlOffset = new Vector3(
                0.5f * Mathf.Cos(angle),
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            ) * swirlAmplitude * damping;

            // Optional extra vertical float during the move (scales with smoothT)
            float verticalOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude * smoothT;

            // Update position
            transform.position = basePos + swirlOffset + new Vector3(0f, verticalOffset, 0f);
            // Interpolate rotation over the duration.
            transform.rotation = Quaternion.Slerp(startRot, endRot, smoothT);

            elapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure final values are set.
        transform.position = endPos;
        transform.rotation = endRot;
        onComplete?.Invoke();
        isFloating = false;
    }
}
