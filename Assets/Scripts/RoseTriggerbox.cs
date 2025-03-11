// Example implementation of IEventReceiver
using UnityEngine;

public class RoseTriggerbox : MonoBehaviour, IRayEventReceiver
{
    [SerializeField]
    private PlanterController controller;

    public void OnRaycastEnter()
    {
        controller.ShowNoteFromStranger();
    }

    public void OnRaycastExit()
    {
    }
}
