using UnityEngine;
using System.Collections;
using TMPro;

public class LetterScript : MonoBehaviour
{
    public float slidePercentage = 0.7f; // how much percentage of the letter's height we want to slide out
    private float slideAmount = 0.5f;
    public float slideDuration = 1f;
    private Vector3 originalPosition;
    internal bool isOut = false;
    internal bool isSliding = false;
    public TextMeshPro textComponent;

    void OnEnable()
    {
        StartCoroutine(InitializeLetterAfterGameManager());
    }

    private IEnumerator InitializeLetterAfterGameManager()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        string message = GameManager.Instance.GetRandomMessage();
        setMessage(message);

        originalPosition = transform.position;
        Renderer renderer = GetComponent<Renderer>();
        
        if (renderer != null)
        {
            float height = renderer.bounds.size.y;
            slideAmount = height * slidePercentage;
        }
        else
        {
            Debug.LogWarning("Renderer not found! Using default slide value.");
            slideAmount = 0.03f; // Fallback value
        }
    }

    internal void SlideLetterOut()
    {
        if (!isOut && !isSliding)
        {
            isSliding = true;
            transform.position = originalPosition;
            StartCoroutine(SlideLetter(transform.position, originalPosition + transform.up * slideAmount));
        }
    }

    internal void SlideLetterIn()
    {
        if (isOut && !isSliding)
        {
            isSliding = true;
            StartCoroutine(SlideLetter(transform.position, originalPosition));
        }
    }
    private void setMessage(string msg)
    {
        Debug.Log("Setting message: " + msg);
        if (textComponent != null)
        {
            Debug.Log("Text component is not null");
            textComponent.text = msg;
        }
    }

    IEnumerator SlideLetter(Vector3 startPosition, Vector3 endPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;
        isOut = !isOut;
        isSliding = false;
    }
}
