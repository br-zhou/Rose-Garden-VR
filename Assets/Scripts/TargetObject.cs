// Example implementation of IEventReceiver
using UnityEngine;

public class TargetObject : MonoBehaviour, IRayEventReceiver
{
    public void OnRaycastEnter()
    {
        Debug.Log("Ray Cast Enter");

    }

    public void OnRaycastExit()
    {
        Debug.Log("Ray Cast Exit");
    }
}
