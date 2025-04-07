using UnityEngine;
using System.Collections;

// Deactivate Envelope:
// Case 1: No animation is playing on the envelope
//         → Set lastTime = true and call SwitchEnvelope()
// Case 2: Some animation is playing on the envelope
//         Case 2-1: The envelope hasn't finished rotating
//                   → Set lastTime = true and it will automatically deactivate itself
//         Case 2-2: The envelope has started to open
//                   → (Idea1) Set oneMoreTime ans it will automatically deactivate itself in the next call
//                   → (Idea2) Close the envelope smoothly and then rotate

// Controls envelop rotations and animations
public class EnvelopeControllerScript : MonoBehaviour
{
    [Header("Objects and Scripts")]
    public GameObject frontEnvelope;
    public GameObject backEnvelope;
    public GameObject frontLetter;
    private EnvelopeScript envelopScript;
    private LetterScript letterScirpt;

    [Header("Envelope Rotation Settings")]
    public float rotationDuration = 0.75f;
    public float rotationAngle = 100f;
    private Quaternion originalRotation;
    private Renderer[] frontEnvelopeRenderers;
    private Renderer[] backEnvelopeRenderers;

    private PlanterController activePlanter;
    private string planterType = null;
    internal float envelopeOffset = 0f;


    private bool firstTime = true;
    private bool lastTime = false;
    private bool canSwitch = true;
    private bool oneMoreTime = false;
    private bool isTooLateToDeactivate = false;
    private bool isAlreadyDeactivating = false;

    void Start()
    {
        envelopScript = frontEnvelope.GetComponent<EnvelopeScript>();
        letterScirpt = frontLetter.GetComponent<LetterScript>();

        originalRotation = transform.rotation;

        // Get the renderers to control the transparency of the envelopes
        frontEnvelopeRenderers = frontEnvelope.GetComponentsInChildren<Renderer>();
        backEnvelopeRenderers = backEnvelope.GetComponentsInChildren<Renderer>();

        // Set the back envelope to the default rotation
        backEnvelope.transform.RotateAround(
            transform.position,
            transform.forward,             
            rotationAngle              
        );

        ResetStatus();
    }

    // main function to switch (close current -> rotate -> open next) the envelopes
    internal void SwitchEnvelop()
    {
        if (canSwitch)
            StartCoroutine(SwitchEnvelopeCoroutine()); 
    }

    internal void setLastTime(bool lastTime)
    {
        this.lastTime = lastTime;
    }

    IEnumerator SwitchEnvelopeCoroutine()
    {
        canSwitch = false;

        if (firstTime && activePlanter != null)
        {
            activePlanter.DeactivateDescriptionPanel();
        }

        // Don't close the envelop if it's the first time
        if (!firstTime)
            // Close the current envelope
            yield return StartCoroutine(CloseEnvelopeCoroutine());

        isTooLateToDeactivate = true;
        // Rotate the envelopes
        yield return StartCoroutine(RotateEnvelopeCoroutine());

        // Open the next envelope
        if (!this.lastTime)
            yield return StartCoroutine(OpenEnvelopeCoroutine());
        isTooLateToDeactivate = false;

        if (firstTime)
            firstTime = false;

        if (oneMoreTime)
        {
            yield return new WaitForSeconds(0.5f);
            oneMoreTime = false;
            yield return StartCoroutine(SwitchEnvelopeCoroutine());
        }

        if (this.lastTime && planterType == "center")
            MoveDownEnvelope(envelopeOffset);

        canSwitch = true;
    }

    IEnumerator RotateEnvelopeCoroutine()
    {
        float time = 0f;
        Quaternion endRotation = originalRotation * Quaternion.Euler(0f, 0f, -rotationAngle);
        // Disable the front letter so that it cannot be seen during the rotation
        frontLetter.SetActive(false);
        if (!lastTime)
            backEnvelope.SetActive(true);

        while (time < rotationDuration)
        {
            float t = Mathf.Lerp(0, 1, time / rotationDuration);

            // Rotate the envelopes
            transform.rotation = Quaternion.Slerp(originalRotation, endRotation, t);

            // Change the transparencies of the envelopes
            SetAlpha(frontEnvelopeRenderers, 1 - t);
            SetAlpha(backEnvelopeRenderers, t);

            time += Time.deltaTime;
            yield return null;
        }

        // Bring both envelopes back to the default position to start the opening animation
        transform.rotation = originalRotation;

        SetAlpha(frontEnvelopeRenderers, 1);
        SetAlpha(backEnvelopeRenderers, 0);

        backEnvelope.SetActive(false);
        if (lastTime)
            frontEnvelope.SetActive(false);
    }

    // Opening Envelope Sequence (open envelope -> slide letter out) 
    IEnumerator OpenEnvelopeCoroutine()
    {
        if (firstTime)
            frontEnvelope.SetActive(true);
        frontLetter.SetActive(true);

        envelopScript.OpenEnvelope();
        yield return new WaitForSeconds(1.5f);

        letterScirpt.SlideLetterOut();
        while (letterScirpt.isSliding) yield return null;
    }

    // Closing Envelope Sequence (slide letter in -> close envelope)
    IEnumerator CloseEnvelopeCoroutine()
    {
        letterScirpt.SlideLetterIn();
        while (letterScirpt.isSliding) yield return null;
        
        envelopScript.CloseEnvelope();
        yield return new WaitForSeconds(1.5f);
    }

    internal void ActivatePlanter(PlanterController planter, string planterType)
    {
        activePlanter = planter;
        this.planterType = planterType;
        if (planterType == "center")
            MoveUpEnvelope(envelopeOffset);
        ResetStatus();
    }

    internal void DeactivatePlanter()
    {
        if (isAlreadyDeactivating) return;
        isAlreadyDeactivating = true;

        if (isTooLateToDeactivate)
            oneMoreTime = true;
        setLastTime(true);
        SwitchEnvelop();
        activePlanter = null;
    }

    internal void MoveUpEnvelope(float distance)
    {
        Debug.Log("Moving up envelope");
        transform.position += Vector3.up * distance;
    }

    internal void MoveDownEnvelope(float distance)
    {
        transform.position -= Vector3.up * distance;
    }
    

    // Reset the status of the envelopes every time planter is activated
    private void ResetStatus() {
        canSwitch = true;
        firstTime = true;
        lastTime = false;
        isTooLateToDeactivate = false;
        oneMoreTime = false;
        isAlreadyDeactivating = false;
        // Set both envelopes to be invisible at first
        SetAlpha(backEnvelopeRenderers, 0);
        frontEnvelope.SetActive(false);
        backEnvelope.SetActive(false);
    }

    // Helper function to set the transparency of each renderer in the argument
    private void SetAlpha(Renderer[] renderers, float alpha)
    {
        foreach (Renderer r in renderers)
        {
            if (r.gameObject.name == "LetterText") continue;
            Color c = r.material.color;
            c.a = alpha;
            r.material.color = c;
        }
    }
}