using UnityEngine;
using System.Collections;

// Controls envelop rotations and animations
public class EnvelopeControllerScript : MonoBehaviour
{
    [Header("Objects and Scripts")]
    public GameObject frontEnvelope;
    public GameObject backEnvelope;
    public GameObject frontLetter;
    public EnvelopeScript envelopScript;
    public LetterScript letterScirpt;

    [Header("Envelope Rotation Settings")]
    public float rotationDuration = 0.85f;
    public float rotationAngle = 100f;
    private Quaternion originalRotation;
    private Renderer[] frontEnvelopeRenderers;
    private Renderer[] backEnvelopeRenderers;

    private bool firstTime = true; // 
    private bool canSwitch = true; // 

    void Start()
    {
        // Save the default rotation
        originalRotation = transform.rotation;

        // Get the renderers to control the transparency of the envelopes
        frontEnvelopeRenderers = frontEnvelope.GetComponentsInChildren<Renderer>();
        backEnvelopeRenderers = backEnvelope.GetComponentsInChildren<Renderer>();

        // Set the back envelope to the default rotation
        backEnvelope.transform.RotateAround(
            transform.position,
            transform.right,             
            -rotationAngle              
        );

        // Set both envelopes to be invisible at first
        SetAlpha(backEnvelopeRenderers, 0);
        frontEnvelope.SetActive(false);
        backEnvelope.SetActive(false);
    }

    // main function to switch (close current -> rotate -> open next) the envelopes
    internal void SwitchEnvelop()
    {
        if (canSwitch)
            StartCoroutine(SwitchEnvelopeCoroutine()); 
    }

    IEnumerator SwitchEnvelopeCoroutine()
    {
        canSwitch = false;

        // Don't close the envelop if it's the first time
        if (!firstTime)
            // Close the current envelope
            yield return StartCoroutine(CloseEnvelopeCoroutine());

        // Rotate the envelopes
        yield return StartCoroutine(RotateEnvelopeCoroutine());

        // Open the next envelope
        yield return StartCoroutine(OpenEnvelopeCoroutine());

        if (firstTime)
            firstTime = false;
        canSwitch = true;
    }

    IEnumerator RotateEnvelopeCoroutine()
    {
        float time = 0f;
        Quaternion endRotation = originalRotation * Quaternion.Euler(rotationAngle, 0f, 0f);
        // Disable the front letter so that it cannot be seen during the rotation
        frontLetter.SetActive(false);
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

        if (firstTime)
            frontEnvelope.SetActive(true);
        frontLetter.SetActive(true);
        backEnvelope.SetActive(false);

        SetAlpha(frontEnvelopeRenderers, 1);
        SetAlpha(backEnvelopeRenderers, 0);
    }

    // Opening Envelope Sequence (open envelope -> slide letter out) 
    IEnumerator OpenEnvelopeCoroutine()
    {
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

    // Helper function to set the transparency of each renderer in the argument
    private void SetAlpha(Renderer[] renderers, float alpha)
    {
        foreach (Renderer r in renderers)
        {
            Color c = r.material.color;
            c.a = alpha;
            r.material.color = c;
        }
    }
}