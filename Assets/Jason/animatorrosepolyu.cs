using UnityEngine;

public class FlowerAnimations : MonoBehaviour
{
    public Animator flowerAnimator; // Reference to the flower animator

    void Update()
    {
        // Check if the "2" key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Seen animation
            flowerAnimator.SetBool("seen", true);
            flowerAnimator.SetBool("lookaway", false);
        }

        // Check if the "3" key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Lookaway animation
            flowerAnimator.SetBool("seen", false);
            flowerAnimator.SetBool("lookaway", true);
        }
    }
}
