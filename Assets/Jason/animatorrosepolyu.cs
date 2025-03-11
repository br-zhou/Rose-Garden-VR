using UnityEngine;

public class FlowerAnimationController : MonoBehaviour
{
    public Animator flowerAnimator; // Reference to the flower animator

    public void playBloom()
    {
        flowerAnimator.SetBool("seen", true);
        flowerAnimator.SetBool("lookaway", false);
    }

    public void playUnbloom()
    {
        flowerAnimator.SetBool("seen", false);
        flowerAnimator.SetBool("lookaway", true);
    }
}
