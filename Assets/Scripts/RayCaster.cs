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
    }

    void PerformRaycast()
    {
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range), Color.red);

        RaycastHit hit;
        if (Physics.Raycast(theRay, out hit, range))
        {
            IRayEventReceiver receiver = hit.collider.GetComponent<IRayEventReceiver>();
            if (receiver == lastReceiver) return;
            if (lastReceiver != null)
            {
                lastReceiver.OnRaycastExit();
            }
            if (receiver != null)
            {
                receiver.OnRaycastEnter();
            }
            lastReceiver = receiver;
        } else
        {
            if (lastReceiver != null)
            {
                lastReceiver.OnRaycastExit();
                lastReceiver = null;
            }
        }
    }

}
