using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeRow : MonoBehaviour
{
    public GameObject lockOverlay;
    public Button unlockButton;
    public Text unlockCost;

    public Image icon;
    public Text title;
    public Text topLevel;
    public Text description;
    public Text currCPH;
    public Text nextCPH;
    public Text currLvl;
    public Text nextLvl;
    public Text cost;

    public Button upgradeButton;

    public UpgradeData selectedUpgrade;

    public ulong currentCost;
    public int upgradeAmt;

    private ObjectManager objectManager;

    private void Update()
    {
        if (selectedUpgrade.level > 0)
        {
            HideLock();
            if (PlayerManager.instance.currentMachine.machineData.isCurrentEvent)
            {
                if (PlayerManager.instance.eventCoins < currentCost)
                {
                    upgradeButton.interactable = false;
                }
                else
                {
                    upgradeButton.interactable = true;
                }
            }
            else
            {
                if (PlayerManager.instance.playerCoins < currentCost)
                {
                    upgradeButton.interactable = false;
                }
                else
                {
                    upgradeButton.interactable = true;
                }
            }
        }
        else
        {
            ShowLock();

            if (PlayerManager.instance.currentMachine.machineData.isCurrentEvent)
            {
                if (PlayerManager.instance.eventCoins < selectedUpgrade.baseCost)
                {
                    unlockButton.interactable = false;
                }
                else
                {
                    unlockButton.interactable = true;
                }
            }
            else
            {
                if (PlayerManager.instance.playerCoins < selectedUpgrade.baseCost)
                {
                    unlockButton.interactable = false;
                }
                else
                {
                    unlockButton.interactable = true;
                }
            }
        }
    }

    public void SetUpgrade(ObjectManager manager)
    {
        objectManager = manager;
        selectedUpgrade = manager.upgradeData;

        unlockCost.text = PlayerManager.instance.numFormat.Format(selectedUpgrade.baseCost);

        icon.sprite = manager.upgradeData.upgradeIcon;
        title.text = manager.upgradeData.objectName;
        description.text = manager.upgradeData.upgradeDesc.Replace("{JackpotMultiplier}", selectedUpgrade.jackpotMultiplier.ToString() + "x");

        topLevel.text = manager.upgradeData.level.ToString() + "/" + manager.upgradeData.unlockLevelData.unlocks[manager.upgradeData.currentUnlockStage].level;
        currLvl.text = manager.upgradeData.level.ToString();
        currCPH.text = PlayerManager.instance.numFormat.Format(selectedUpgrade.GetProductionValue(selectedUpgrade.level));

        SetCostByAmount(1);

        if (PlayerManager.instance.currentMachine.machineData.isCurrentEvent)
        {
            if (PlayerManager.instance.eventCoins < currentCost)
            {
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.interactable = true;
            }
        }
        else
        {
            if (PlayerManager.instance.playerCoins < currentCost)
            {
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.interactable = true;
            }
        }
    }

    public void SetCostByAmount(int amount)
    {
        // UI Changes
        nextLvl.text = (selectedUpgrade.level + amount).ToString();
        nextCPH.text = PlayerManager.instance.numFormat.Format(selectedUpgrade.GetProductionValue(selectedUpgrade.level + amount));

        // Cost Calculations
        float currentLevel = Mathf.Pow(selectedUpgrade.upgradeCostGrowthRate, selectedUpgrade.level);
        float numToBuy = (Mathf.Pow(selectedUpgrade.upgradeCostGrowthRate, amount) - 1);
        float topNum = currentLevel * numToBuy;
        float bottomNum = selectedUpgrade.upgradeCostGrowthRate - 1;

        currentCost = (ulong)(selectedUpgrade.baseCost * (topNum / bottomNum));
        cost.text = PlayerManager.instance.numFormat.Format(currentCost);
        upgradeAmt = amount;

    }

    public void BuyUpgrade()
    {
        if (selectedUpgrade.level == 0)
        {
            objectManager.ShowObjects();
        }

        PlayerManager.instance.RemoveCoins(currentCost);
        selectedUpgrade.LevelUp(upgradeAmt);
        topLevel.text = objectManager.upgradeData.level.ToString() + "/" + objectManager.upgradeData.unlockLevelData.unlocks[objectManager.upgradeData.currentUnlockStage].level;
        currLvl.text = selectedUpgrade.level.ToString();
        currCPH.text = PlayerManager.instance.numFormat.Format(selectedUpgrade.GetProductionValue(selectedUpgrade.level));
        SetCostByAmount(upgradeAmt);
    }

    public void BuyUnlock()
    {
        PlayerManager.instance.RemoveCoins(selectedUpgrade.baseCost);
        selectedUpgrade.level++;
        objectManager.ShowObjects();
        HideLock();
    }

    public void ShowLock()
    {
        lockOverlay.SetActive(true);
    }

    public void HideLock()
    {
        lockOverlay.SetActive(false);
    }
}
