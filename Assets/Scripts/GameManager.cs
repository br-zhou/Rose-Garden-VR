using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int state = -1;
    private PopupEvent currentPopup = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    public void setCurrentPopup(PopupEvent newPopup)
    {
        currentPopup = newPopup;
    }

    public void closeCurrentPopup()
    {
        if (currentPopup == null) return;

        currentPopup.ClosePopup();
        currentPopup = null;
    }
}