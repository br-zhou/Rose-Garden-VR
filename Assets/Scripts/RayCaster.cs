using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public float range = 5;
    public IRayEventReceiver lastReceiver = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        PerformRaycast();

        GameManager.Instance.GetGuiController().SetHoverState(lastReceiver != null);
    }

    void PerformRaycast()
    {
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        //Debug.DrawRay(transform.position, transform.TransformDirection(direction * range), Color.red);

        RaycastHit[] hits = Physics.RaycastAll(theRay, range);

        foreach (var hit in hits)
        {
            IRayEventReceiver receiver = hit.collider.GetComponent<IRayEventReceiver>();
            if (receiver == null) continue;
            if (receiver == lastReceiver) return;
            if (GameManager.Instance.isInstructionActive && receiver.GetType() != typeof(StartButton)) return;
            if (lastReceiver != null)
            {
                lastReceiver.OnRaycastExit();
            }
            if (receiver != null && receiver.CanReceiveRays())
            {
                receiver.OnRaycastEnter();
                lastReceiver = receiver;
                return;
            }
        }

        // no hits
        if (lastReceiver != null)
        {
            lastReceiver.OnRaycastExit();
            lastReceiver = null;
        }
        
    }

    public void Activate()
    {
        if (lastReceiver != null)
        {
            lastReceiver.Activate();
        }
    }

    public void DeActivate()
    {
        if (lastReceiver != null)
        {
            lastReceiver.DeActivate();
        }
    }

}
