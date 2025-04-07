using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlanterController : MonoBehaviour
{
    public GameObject descriptionPanel;
    public EnvelopeControllerScript envelopeController;
    public DescriptionPanelController descriptionPanelController;
    public RoseController roseController;

    void Start()
    {
        
    }

    public virtual void setIsActive(bool value)
    {
        Debug.Log("called setIsActive (base)");
        if (value) {
            ActivatePlanter();
        } else {
            DeactivatePlanter();
        }
    }

    internal void ActivatePlanter(string planterType = "side")
    {
        roseController.SetIsActiveRoses(true);
        envelopeController.ActivatePlanter(this, planterType);
        descriptionPanelController.ActivatePanel(descriptionPanel, planterType);
    }

    internal void DeactivatePlanter()
    {
        roseController.SetIsActiveRoses(false);
        envelopeController.DeactivatePlanter();
        descriptionPanelController.DeactivatePanel();
    }

    public void DeactivateDescriptionPanel()
    {
        descriptionPanelController.DeactivatePanel();
    }
}
