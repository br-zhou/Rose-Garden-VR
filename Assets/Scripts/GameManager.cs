using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<IRayEventReceiver> popupList;
    private DatabaseManager dbm;

    [SerializeField]
    GuiController guiController;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
        dbm = GetComponent<DatabaseManager>();
        popupList = new List<IRayEventReceiver>();
    }

    public void setCurrentRayAction(IRayEventReceiver e)
    {
        popupList.Add(e);
    }

    public GuiController GetGuiController()
    {
        return guiController;
    }

    public void closeCurrentRayAction()
    {
        if (popupList.Count == 0) return;

        IRayEventReceiver lastItem = popupList[popupList.Count - 1];
        lastItem.DeActivate();
        popupList.RemoveAt(popupList.Count - 1);
    }

    public void GoBack()
    {
        closeCurrentRayAction();
    }

    public string GetRandomMessage()
    {
        return dbm.GetRandomMessage();
    }

    public bool isFocused()
    {
        return popupList.Count > 0;
    }
}