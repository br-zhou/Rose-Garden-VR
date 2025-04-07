// Example implementation of IEventReceiver
using UnityEngine;

public class RoseTriggerbox : MonoBehaviour, IRayEventReceiver
{
    // [SerializeField]
    // private PlanterController controller;
    public bool isActive = false;

    public void OnRaycastEnter()
    {
    }

    public void OnRaycastExit()
    {

    }

    public void Activate()
    {
        GameManager.Instance.setCurrentRayAction(this);
        // controller.ShowNoteFromStranger();
    }

    public void DeActivate()
    {
        // controller.HideNoteFromStranger();
    }

    public bool CanReceiveRays()
    {
        return isActive;
    }
}
