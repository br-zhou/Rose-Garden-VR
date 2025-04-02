using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    public PlanterScript planterScript;
    public EnvelopeControllerScript envelopeControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("GameManager Start");
    }

    // Update is called once per frame
    void Update()
    {
        // Envelop Related
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            envelopeControllerScript.SwitchEnvelop();
        }

        // Planter Related
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            // only at most one of the following will be called
            planterScript.MovePlanterToTarget();
            planterScript.MovePlanterToOriginal();
        }
    }
}
