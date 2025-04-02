// Example implementation of IEventReceiver
using Unity.VisualScripting;
using UnityEngine;

public class PlanterController : MonoBehaviour
{
    private bool isActive = false;

    [SerializeField]
    EnvelopeController envelopeController;
    [SerializeField]
    GameObject noteObj;
    [SerializeField]
    private FlowerAnimationController flowerAnimator;
    [SerializeField]
    RoseTriggerbox rtb;

    private bool showingNote = false;
    public void ShowNoteFromStranger()
    {
        //if (!isActive) return;

        //noteObj.SetActive(true);
        //flowerAnimator.playBloom();
        showingNote = true;
        envelopeController.toggleEnvelope(true);
    }

    public void HideNoteFromStranger()
    {
        //flowerAnimator.playUnbloom();
        //noteObj.SetActive(false);
        showingNote = false;
        envelopeController.toggleEnvelope(false);
    }

    public void setIsActive(bool value)
    {
        isActive = value;
        rtb.isActive = value;

        //if (value == false && showingNote)
        //{
        //    HideNoteFromStranger();
        //}
    }
}
