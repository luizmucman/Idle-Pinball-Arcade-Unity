using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeasonPassRewardItem", menuName = "ScriptableObjects/SeasonPass/RewardItem")]
public class SeasonPassItemSO : ScriptableObject
{
    public string rewardTitle;
    public string rewardDesc;
    public Sprite rewardIcon;

    public RewardType rewardType;

    [Header("If Gem Reward")]
    // If gem reward
    public int gems;

    [Header("If Specific Boost Reward")]
    public BoostData boostData;

    public void GetItem()
    {
        UIShopManager shopManager = UIManager.instance.uiShopManager;

        switch (rewardType)
        {
            case RewardType.EventMachineUnlock:
                UnlockEventMachine();
                break;
            case RewardType.CommonBallChest:
                shopManager.RewardCommonChest(ShopItemType.Ball);
                break;
            case RewardType.EpicBallChest:
                shopManager.RewardEpicChest(ShopItemType.Ball);
                break;
            case RewardType.CommonTicketChest:
                shopManager.RewardCommonChest(ShopItemType.Ticket);
                break;
            case RewardType.EpicTicketChest:
                shopManager.RewardEpicChest(ShopItemType.Ticket);
                break;
            case RewardType.Gems:
                shopManager.BuyGems(gems);
                break;
            case RewardType.Boost:
                RewardSpecificBoost();
                break;
        }

    }

    private void UnlockEventMachine()
    {
        PlayerManager.instance.currentEventMachineData.isUnlocked = true;
    }

    private void RewardSpecificBoost()
    {
        PlayerManager.instance.AddBoost(boostData);
    }
}

public enum RewardType
{
    EventMachineUnlock,
    CommonBallChest,
    EpicBallChest,
    CommonTicketChest,
    EpicTicketChest,
    MysteryBoost,
    MegaMysteryBoost,
    Boost,
    Ball,
    Ticket,
    Coins,
    Gems
}

