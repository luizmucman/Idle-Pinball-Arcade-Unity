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

    [Header("Base values")]
    // Base Values
    public ulong baseCoinProduction;
    public ulong baseCost;

    [Header("Multiplier Rates")]
    public float upgradeCostGrowthRate;
    public float cpsMultiplier;

    [Header("Current Values")]
    public int level;
    public ulong currentCoinProduction;
    public ulong currentCost;
    public ulong currentCPS;

    [Header("Physics Values")]
    public float bounceForce;

    public void SetData()
    {
        currentCoinProduction = GetProductionValue(level);
        currentCost = (ulong)(baseCost * Mathf.Pow(upgradeCostGrowthRate, (float)level));
        currentCPS = (ulong) (currentCoinProduction * cpsMultiplier);
    }

    public ulong GetProductionValue(int level)
    {
        return baseCoinProduction * (ulong)level;
    }
}
