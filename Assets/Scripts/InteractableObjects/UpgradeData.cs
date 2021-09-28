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
    public ulong baseCoinProduction;
    public ulong baseCost;
    public float upgradeCostGrowthRate;
    public float jackpotMultiplier;

    [Header("Current Values")]
    public int level;
    public ulong currentCoinProduction;
    public ulong currentCost;

    [Header("UnlockData")]
    public ManagerUnlocks unlockLevelData;
    public int currentUnlockStage;
    public ulong currentUnlockMultiplier;

    public void SetData()
    {
        if (currentUnlockMultiplier == 0)
        {
            currentUnlockMultiplier = 1;
        }
        currentCoinProduction = GetProductionValue(level);
        currentCost = (ulong)(baseCost * Mathf.Pow(upgradeCostGrowthRate, (float)level));
    }

    public ulong GetProductionValue(int level)
    {
        return baseCoinProduction * (ulong)level * currentUnlockMultiplier;
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
}
