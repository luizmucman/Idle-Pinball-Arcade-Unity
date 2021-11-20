using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectManager : MonoBehaviour
{

    private MachineManager machineManager;
    public ChallengeType challengeType;
    public bool hasChallenge;

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
        iObjects = GetComponentsInChildren<InteractableObject>();
        machineManager = GetComponentInParent<MachineManager>();
    }

    public virtual void Start()
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
        if(hasChallenge)
        {
            UIManager.instance.uiChallengeManager.AddChallengeHit(1, challengeType);
        }

        double coinGain = 0;
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

        coinGain = PlayerManager.instance.AddCoins((double)(upgradeData.currentCoinProduction));
        PlayerManager.instance.currentMachine.coinsPerSecondCounter += (double)(coinGain);
        PlayerManager.instance.currentMachine.machineData.totalCoinsGained += coinGain;
        ball.accumulatedCoins += coinGain;
    }

    public virtual void Payout(float multiplier)
    {
        if (hasChallenge)
        {
            UIManager.instance.uiChallengeManager.AddChallengeHit(1, challengeType);
        }

        double coinGain = 0;
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
        coinGain = PlayerManager.instance.AddCoins((double)(upgradeData.currentCoinProduction));
        PlayerManager.instance.currentMachine.coinsPerSecondCounter += (double)(upgradeData.currentCoinProduction);
        PlayerManager.instance.currentMachine.machineData.totalCoinsGained += coinGain;
    }

    public void EnableUpgradeCanvas()
    {
        
    }

    public void DisableUpgradeCanvas()
    {
        
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
