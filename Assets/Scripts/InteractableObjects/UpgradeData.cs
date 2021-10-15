using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    [Header("UI Elements")]
    // UI
    public Sprite upgradeIcon;
    public string objectName;
    public string upgradeDesc;

    [Header("Set Values")]
    // Base Values
    public double baseCoinProduction;
    public double baseCost;
    public float upgradeCostGrowthRate;
    public float jackpotMultiplier;

    [Header("Current Values")]
    public int level;
    public double currentCoinProduction;
    public double currentCost;

    [Header("UnlockData")]
    public ManagerUnlocks unlockLevelData;
    public int currentUnlockStage;
    public double currentUnlockMultiplier;

    public void SetData()
    {
        if (currentUnlockMultiplier == 0)
        {
            currentUnlockMultiplier = 1;
        }
        currentCoinProduction = GetProductionValue(level);
        currentCost = (double)(baseCost * Mathf.Pow(upgradeCostGrowthRate, (float)level));
    }

    public double GetProductionValue(int level)
    {
        return baseCoinProduction * level * currentUnlockMultiplier;

    }

    public void LevelUp(int upgradeAmt)
    {
        level += upgradeAmt;
        while(level > unlockLevelData.unlocks[currentUnlockStage].level)
        {
            currentUnlockMultiplier *= unlockLevelData.unlocks[currentUnlockStage].multiplier;
            currentUnlockStage++;
        }
        SetData();
    }

    public void SaveData(string machineSceneName)
    {
        ES3.Save(machineSceneName + objectName + "-level", level);
    }

    public void LoadData(string machineSceneName)
    {
        if (ES3.KeyExists(machineSceneName + objectName + "-level"))
        {
            level = ES3.Load(machineSceneName + objectName + "-level", 0);
            SetData();
        }
    }
}
