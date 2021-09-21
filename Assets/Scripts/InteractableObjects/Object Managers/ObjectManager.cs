using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectManager : MonoBehaviour
{

    // Upgrade Data
    public UpgradeData upgradeData;

    // Canvas Elements
    private Canvas upgradeBtnCanvas;

    // Interactable Objects List
    private InteractableObject[] iObjects;

    public float jackpotMultiplier;

    public ObjectManager dependentManager;

    public virtual void Awake()
    {
        upgradeData.SetData();
        upgradeBtnCanvas = GetComponentInChildren<Canvas>();
        upgradeBtnCanvas.enabled = false;
        iObjects = GetComponentsInChildren<InteractableObject>();
    }

    public void Start()
    {
        CheckIfObjectOwned();
    }

    public virtual void Payout(Ball ball, float multiplier)
    {
        PlayerManager.instance.AddCoins((ulong)(upgradeData.currentCoinProduction));
        PlayerManager.instance.currentMachine.coinsPerSecondCounter += (ulong)(upgradeData.currentCoinProduction);
    }

    public virtual void Payout(float multiplier)
    {
        PlayerManager.instance.AddCoins((ulong)(upgradeData.currentCoinProduction));
        PlayerManager.instance.currentMachine.coinsPerSecondCounter += (ulong)(upgradeData.currentCoinProduction);
    }

    public void EnableUpgradeCanvas()
    {
        if (dependentManager != null)
        {
            if (dependentManager.upgradeData.level > 0)
            {
                upgradeBtnCanvas.enabled = true;
            }
            else
            {
                upgradeBtnCanvas.enabled = false;
            }
        }
        else
        {
            upgradeBtnCanvas.enabled = true;
        }
    }

    public void DisableUpgradeCanvas()
    {
        upgradeBtnCanvas.enabled = false;
    }

    public void SaveManager(string machineName)
    {
        ES3.Save(machineName + gameObject.name, upgradeData);
    }

    public void LoadManager(string machineName)
    {
        if(ES3.KeyExists(machineName + gameObject.name))
        {
            ES3.LoadInto(machineName + gameObject.name, upgradeData);
        }
    }

    public void CheckIfObjectOwned()
    {
        
        if(upgradeData.level > 0)
        {
            ShowObjects();
        }
        else
        {
            HideObjects();
        }
    }

    public void ShowObjects()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            GameObject currGo = transform.GetChild(i).gameObject;
            if (currGo.GetComponent<Canvas>() == null)
            {
                currGo.SetActive(true);
            }
        }
    }

    public void HideObjects()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            GameObject currGo = transform.GetChild(i).gameObject;
            if (currGo.GetComponent<Canvas>() == null)
            {
                currGo.SetActive(false);
            }
        }
    }
}
