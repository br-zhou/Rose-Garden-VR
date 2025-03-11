// Example implementation of IEventReceiver
using UnityEngine;

public class CancelTrigger : MonoBehaviour, IRayEventReceiver
{
    public void OnRaycastEnter()
    {
        GameManager.Instance.closeCurrentPopup();
    }

    public void OnRaycastExit()
    {

    }
}
