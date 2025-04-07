using UnityEngine;

public class RoseScript : MonoBehaviour, IRayEventReceiver
{
    public EnvelopeControllerScript envelopeControllerScript;
    private bool isActive = false;
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void OnRaycastEnter() {}
    public void OnRaycastExit() {}

    public void Activate()
    {
        if (!GameManager.Instance.isFocused()) return;
        if (GameManager.Instance.setCurrentRayAction(this))
            envelopeControllerScript.SwitchEnvelop();
    }

    public void DeActivate()
    {
    }

    public void SetIsActive(bool isActive)
    {
        this.isActive = isActive;
        if (isActive && animator != null)
        {
            animator.SetTrigger("OpenRoseTrigger");
        }
        else if (!isActive && animator != null)
        {
            animator.SetTrigger("CloseRoseTrigger");
        }
    }

    public bool CanReceiveRays()
    {
        return isActive;
    }
}
