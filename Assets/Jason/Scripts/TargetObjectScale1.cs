using UnityEngine;

public class TargetObjectRotate : MonoBehaviour, IRayEventReceiver
{
    public float rotationSpeed = 180f;  // Made public
    public float decelerationRate = 180f;  // Made public

    private float currentSpeed = 0f;
    private bool isBeingWatched = false;

    private void Update()
    {
        if (isBeingWatched)
        {
            currentSpeed = rotationSpeed;
        }
        else if (currentSpeed > 0)
        {
            currentSpeed = Mathf.Max(0, currentSpeed - decelerationRate * Time.deltaTime);
        }

        transform.Rotate(Vector3.up, currentSpeed * Time.deltaTime);
    }

    public void OnRaycastEnter()
    {
        isBeingWatched = true;
    }

    public void OnRaycastExit()
    {
        isBeingWatched = false;
    }
}
