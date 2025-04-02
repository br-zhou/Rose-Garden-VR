// Example implementation of IEventReceiver
using UnityEngine;

public class CancelTrigger : MonoBehaviour, IRayEventReceiver
{
    public void OnRaycastEnter()
    {
        //GameManager.Instance.closeCurrentPopup();
    }

    public void OnRaycastExit()
    {

    }

    public void Activate()
    {

    }

    public void DeActivate()
    {

    }

    public bool CanReceiveRays()
    {
        return true;
    }
}
