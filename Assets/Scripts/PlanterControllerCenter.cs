using UnityEngine;

public class CentrePlanterController : PlanterController
{
    private float centreOffset = 1f;

    void Start()
    {
        envelopeController.envelopeOffset = this.centreOffset;
        descriptionPanelController.descriptionPanelOffset = this.centreOffset;
    }

    public override void setIsActive(bool value)
    {
        Debug.Log("called setIsActive (centre)");
        if (value) {
            ActivatePlanter("center");
        } else {
            DeactivatePlanter();
        }
    }
}
