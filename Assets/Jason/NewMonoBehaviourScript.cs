using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public Animator cameraAnimator; // Reference to the camera animator

    void Update()
    {
        // Swap animation states based on key presses
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Basic rotation
            cameraAnimator.SetBool("spinonit", false);
            cameraAnimator.SetBool("camerasus", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Camera Yes animation
            cameraAnimator.SetBool("spinonit", true);
            cameraAnimator.SetBool("camerasus", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Camera No animation
            cameraAnimator.SetBool("spinonit", false);
            cameraAnimator.SetBool("camerasus", true);
        }
    }
}
