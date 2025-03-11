using UnityEngine;
using System.Collections;

public class TargetObjectScale : MonoBehaviour, IRayEventReceiver
{
    [SerializeField] private float scaleMultiplier = 1.5f;
    [SerializeField] private float duration = 0.5f;
    
    private Vector3 originalScale;
    private Coroutine scaleRoutine;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnRaycastEnter()
    {
        StartScaling(originalScale * scaleMultiplier);
    }

    public void OnRaycastExit()
    {
        StartScaling(originalScale);
    }

    private void StartScaling(Vector3 targetScale)
    {
        if (scaleRoutine != null)
        {
            StopCoroutine(scaleRoutine);
        }
        scaleRoutine = StartCoroutine(ScaleOverTime(targetScale));
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
            yield return null;
        }

        transform.localScale = targetScale;
        scaleRoutine = null;
    }
}