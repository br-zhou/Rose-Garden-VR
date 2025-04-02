using UnityEngine;
using System.Collections;

public class EnvelopeController : MonoBehaviour
{
    [SerializeField]
    GameObject envelope;
    [SerializeField]
    ParticleSystem smokeEffect;
    [SerializeField]
    LetterScript letterScirpt;

    EnvelopeScript envelopScript;
    bool isVisible = false;

    public void Start()
    {
        envelopScript = envelope.GetComponent<EnvelopeScript>();
        envelope.SetActive(false);
    }

    public void toggleEnvelope(bool val)
    {
        //if (!isVisible && !envelopScript.isOpen && val)
        //{
        //    smokeEffect.Play();
        //    StartCoroutine(OpenSequence());
        //}
        //else if (isVisible && letterScirpt.isOut)
        //{
        //    //envelope.SetActive(false);
        //    StartCoroutine(CloseSequence());
        //}
        if (val)
        {
            smokeEffect.Play();
            StartCoroutine(OpenSequence());
        }
        else
        {
            //envelope.SetActive(false);
            StartCoroutine(CloseSequence());
        }
    }

    // Opening Sequence (make visible -> open envelope -> slide letter out) 
    IEnumerator OpenSequence()
    {
        envelope.SetActive(true);
        isVisible = true;
        envelopScript.OpenEnvelope();
        yield return new WaitForSeconds(1.5f);
        letterScirpt.SlideLetterOut();
        while (letterScirpt.isSliding) yield return null;
    }

    // Closing Sequence (slide letter in -> close envelope -> make invisible)
    IEnumerator CloseSequence()
    {
        letterScirpt.SlideLetterIn();
        while (letterScirpt.isSliding) yield return null;
        envelopScript.CloseEnvelope();
        yield return new WaitForSeconds(1.5f);
        envelope.SetActive(false);
        isVisible = false;
    }
}
