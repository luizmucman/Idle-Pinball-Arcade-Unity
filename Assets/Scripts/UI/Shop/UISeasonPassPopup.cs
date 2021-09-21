using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISeasonPassPopup : MonoBehaviour
{
    // UI Components
    public Text titleText;
    public Image machineImage;
    public Text descText;

    // Season Pass Data
    public string machineGUID;
    public string machineTitle;
    public Sprite machineSprite;
    public string machineDesc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PurchaseSeasonPass()
    {
        UIManager.instance.uiMachineManager.UnlockEventMachine(machineGUID);
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
