using UnityEngine;

public class SealingScript : MonoBehaviour
{
    private Renderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            renderer.material.renderQueue = 3100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
