// Example implementation of IEventReceiver
using Unity.VisualScripting;
using UnityEngine;

public class PlanterController : MonoBehaviour
{
    private bool isActive = false;

    [SerializeField]
    GameObject noteObj;
    [SerializeField]
    private FlowerAnimationController flowerAnimator;

    private bool showingNote = false;
    public void ShowNoteFromStranger()
    {
        if (!isActive) return;

        noteObj.SetActive(true);
        flowerAnimator.playBloom();
        showingNote = true;
    }

    public void HideNoteFromStranger()
    {
        if (showingNote)
        {
            flowerAnimator.playUnbloom();
            noteObj.SetActive(false);
            showingNote = false;
        }
    }

    public void setIsActive(bool value)
    {
        isActive = value;

        if (value == false && showingNote)
        {
            HideNoteFromStranger();
        }
    }
}
