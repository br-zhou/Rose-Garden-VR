using UnityEngine;

public class EnvelopeScript : MonoBehaviour
{
    // Envelop Animator
    private Animator animator = null;
    internal bool isOpen = false;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    internal void OpenEnvelope()
    {
        if (!isOpen)
        {
            animator.SetTrigger("OpenEnvelopeTrigger");
            isOpen = true;
        }
    }

    internal void CloseEnvelope()
    {
        if (isOpen)
        {
            animator.SetTrigger("CloseEnvelopeTrigger");
            isOpen = false;
        }
    }
}