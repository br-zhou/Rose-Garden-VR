// Example implementation of IEventReceiver
using UnityEngine;

public class PlanterController : MonoBehaviour
{
    private bool isActive = false;
    public void ShowNoteFromStranger()
    {
        if (!isActive) return;

        print("HELLO");

    }

    public void setIsActive(bool value)
    {
        isActive = value;
    }
}
