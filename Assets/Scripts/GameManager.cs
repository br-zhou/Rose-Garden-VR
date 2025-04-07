using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<IRayEventReceiver> popupList;
    private DatabaseManager dbm;

    public bool isInstructionActive = true;

    [SerializeField]
    GuiController guiController;


    [SerializeField]
    StartButton startScript;

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

    // true -> success, false -> failed
    public bool setCurrentRayAction(IRayEventReceiver e)
    {
        if (popupList.Count > 0 && popupList[popupList.Count - 1] == e) return false;
        if (popupList.Count > 1) {
            closeCurrentRayAction();
        }
        popupList.Add(e);
        Debug.Log($"Added {e} to popupList");
        return true;
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
        while (popupList.Count > 0)
        {
            closeCurrentRayAction();
        }
    }

    public string GetRandomMessage()
    {
        return dbm.GetRandomMessage();
    }

    public bool isFocused()
    {
        return popupList.Count > 0;
    }
    
    public void ResetInstructions()
    {
        startScript.ResetIns();
    }
}