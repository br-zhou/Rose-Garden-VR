using UnityEngine;

public class RoseController : MonoBehaviour
{
    void Start()
    {
        SetIsActiveRoses(false);
    }
    
    internal void SetIsActiveRoses(bool isActive)
    {
        RoseScript[] roseScripts = this.gameObject.GetComponentsInChildren<RoseScript>();

        foreach (RoseScript roseScript in roseScripts)
        {
            roseScript.SetIsActive(isActive);
        }
    }
}
