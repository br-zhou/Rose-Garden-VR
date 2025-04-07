using UnityEngine;

public class FlyingAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Randomize animation playback speed
        animator.speed = Random.Range(0.8f, 1.2f);

        // Randomize starting point of the animation (from 0 to 1)
        animator.Play("FlyingAnimation", 0, Random.Range(0f, 1f));
    }
}
