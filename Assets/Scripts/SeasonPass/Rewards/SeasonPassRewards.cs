using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeasonPassRewards
{
    public bool isClaimed;

    public SeasonPassItemSO rewardItem;


    public void RedeemReward()
    {
        UIShopManager shopManager = UIManager.instance.uiShopManager;
        
        if(!isClaimed)
        {
            rewardItem.GetItem();
            isClaimed = true;
        }
    }
}

