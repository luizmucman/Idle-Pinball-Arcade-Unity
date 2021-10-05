using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectManager : MonoBehaviour
{

    private MachineManager machineManager;

    // Upgrade Data
    public UpgradeData upgradeData;

    // Canvas Elements
    private Canvas upgradeBtnCanvas;

    // Interactable Objects List
    private InteractableObject[] iObjects;

    public ObjectManager dependentManager;

    // Balance Checks
    [HideInInspector] public float timePassed;
    [HideInInspector] public int timesHit;
    [HideInInspector] public int timesJackpot;


    public virtual void Awake()
    {
        upgradeData.SetData();
        upgradeBtnCanvas = GetComponentInChildren<Canvas>();
        upgradeBtnCanvas.enabled = false;
        iObjects = GetComponentsInChildren<InteractableObject>();
        machineManager = GetComponentInParent<MachineManager>();
    }

    public void Start()
    {
        CheckIfObjectOwned();
        timePassed = 0;
        timesHit = 0;
        timesJackpot = 0;
    }

    private void Update()
    {
        if (machineManager.testBalance)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 3600)
            {
                PrintBalanceTest();
                timePassed = 0;
                timesHit = 0;
                timesJackpot = 0;
            }
        }

    }

    public void PrintBalanceTest()
    {
        Debug.Log(upgradeData.objectName + " Hit Number: " + timesHit);
        if (upgradeData.jackpotMultiplier > 0)
        {
            Debug.Log(upgradeData.objectName + " Times Jackpot: " + timesJackpot);
        }
    }

    public virtual void Payout(Ball ball, float multiplier)
    {
        if (machineManager.testBalance)
        {
            if (multiplier > 1f)
            {
                timesJackpot++;
            } 
            else 
            {
                timesHit++;
            }
        }
        PlayerManager.instance.AddCoins((ulong)(upgradeData.currentCoinProduction));
        PlayerManager.instance.currentMachine.coinsPerSecondCounter += (ulong)(upgradeData.currentCoinProduction);
    }

    public virtual void Payout(float multiplier)
    {
        if (machineManager.testBalance && multiplier > 1f)
        {
            if (multiplier > 1f)
            {
                timesJackpot++;
            }
            else
            {
                timesHit++;
            }
        }
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


    public void SaveManager(string machineName)
    {
        upgradeData.SaveData(machineName);
    }

    public void LoadManager(string machineName)
    {
        upgradeData.LoadData(machineName);
    }
}
