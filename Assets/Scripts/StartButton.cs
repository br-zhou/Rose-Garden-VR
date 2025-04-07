using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButton : MonoBehaviour, IRayEventReceiver
{
    public RawImage instructionImage;
    public float fadeDuration = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Activate()
    {
        StartCoroutine(FadeInInstructionImage());
    }

    IEnumerator FadeInInstructionImage()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(instructionImage.color.a, 0f, t);
            SetAlpha(currentAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetAlpha(0f);
        instructionImage.gameObject.SetActive(false);
        GameManager.Instance.isInstructionActive = false;
    }

    private void SetAlpha(float alpha)
    {
        Color color = instructionImage.color;
        color.a = alpha;
        instructionImage.color = color;
    }
    

    public void DeActivate()
    {

    }

    public bool CanReceiveRays()
    {
        return GameManager.Instance.isInstructionActive;
    }

    public void OnRaycastEnter() {}

    public void OnRaycastExit() {}
}
