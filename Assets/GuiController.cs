using UnityEngine;
using UnityEngine.UI;


public class GuiController : MonoBehaviour
{
    [SerializeField]
    GameObject defaultCursorObj;

    [SerializeField]
    GameObject hoverCursorObj;

    void Start()
    {
        SetHoverState(false);
    }

    public void SetHoverState(bool value)
    {
        defaultCursorObj.SetActive(!value);
        hoverCursorObj.SetActive(value);
    }
}
