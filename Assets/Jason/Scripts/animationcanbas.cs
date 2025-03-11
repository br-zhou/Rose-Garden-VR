using UnityEngine;

public class SetCameraBool : MonoBehaviour
{
    public Animator cameraAnimator; // Reference to the camera animator

    void Update()
    {
        // Check if the "2" key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraAnimator.SetBool("far", false);
            cameraAnimator.SetBool("close", true);
        }

        // Check if the "3" key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraAnimator.SetBool("far", true);
            cameraAnimator.SetBool("close", false);
        }
    }
}
