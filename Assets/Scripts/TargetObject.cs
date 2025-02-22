// Example implementation of IEventReceiver
using UnityEngine;

public class TargetObject : MonoBehaviour, IEventReceiver
{
    int count = 0;
    public void OnRaycastHit()
    {
        Debug.Log("I was hit by a raycast! " + count++);
    }
}
