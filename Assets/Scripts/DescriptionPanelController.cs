using UnityEngine;
using System.Collections;

public class DescriptionPanelController : MonoBehaviour
{
    private GameObject descriptionPanel = null;
    private Coroutine fadeCoroutine;
    private bool isFading = false;

    private bool isDescriptionShown = false;

    public float descriptionPanelFadeDuration = 0.5f;
    private Renderer descriptionPanelRenderer;

    private string planterType = null;
    internal float descriptionPanelOffset = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeactivateAllChildren();
    }

    private void DeactivateAllChildren()
    {
        int childCount = this.gameObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            GameObject child = this.gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }

    internal void ActivatePanel(GameObject panel, string planterType = "side")
    {
        // Register the panel and renderer
        descriptionPanel = panel;
        descriptionPanelRenderer = descriptionPanel.GetComponent<Renderer>();

        // Set the panel to active and set the alpha to 0
        descriptionPanel.SetActive(true);
        SetAlpha(descriptionPanelRenderer, 0f);
        if (planterType == "center")
            MoveUpPanel(descriptionPanelOffset);

        // Start the fade in coroutine
        isDescriptionShown = true;
        StopFadeCoroutineIfExists();
        fadeCoroutine = StartCoroutine(FadeInDescriptionPanel(true, 0f, 1f));
    }

    internal void DeactivatePanel()
    {
        // Fade out the panel
        if (isDescriptionShown)
        {
            StopFadeCoroutineIfExists();
            fadeCoroutine = StartCoroutine(FadeInDescriptionPanel(false, 1f, 0f, () => {
                if (planterType == "center")
                    MoveDownPanel(descriptionPanelOffset);
                descriptionPanel.SetActive(false);
                isDescriptionShown = false;
                descriptionPanel = null;
                descriptionPanelRenderer = null;
            }));
        }
    }

    internal void StopFadeCoroutineIfExists() {
        if (isFading && fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
    }

    internal IEnumerator FadeInDescriptionPanel(
        bool fadeIn,
        float startAlpha,
        float targetAlpha,
        System.Action onComplete = null)
    {
        isFading = true;

        float time = 0f;
        while (time < descriptionPanelFadeDuration) {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / descriptionPanelFadeDuration);
            SetAlpha(descriptionPanelRenderer, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(descriptionPanelRenderer, targetAlpha);
        onComplete?.Invoke();
        isFading = false;
    }

    internal void MoveUpPanel(float distance)
    {
        descriptionPanel.transform.position += Vector3.up * distance;
    }

    internal void MoveDownPanel(float distance)
    {
        Debug.Log("Moving down panel");
        descriptionPanel.transform.position -= Vector3.up * distance;
    }

    // Helper function to set the transparency of a render
    private void SetAlpha(Renderer renderer, float alpha)
    {
        Color c = renderer.material.color;
        c.a = alpha;
        renderer.material.color = c;
    }
}
