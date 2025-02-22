using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public float range = 5;
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
            IEventReceiver receiver = hit.collider.GetComponent<IEventReceiver>();
            if (receiver != null)
            {
                receiver.OnRaycastHit();
            }
        }
    }

}
