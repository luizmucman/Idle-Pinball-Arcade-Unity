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
                shopManager.Reward1Pull(ShopItemType.Ball);
                break;
            case RewardType.EpicBallChest:
                shopManager.Reward10Pulls(ShopItemType.Ball);
                break;
            case RewardType.CommonTicketChest:
                shopManager.Reward1Pull(ShopItemType.Ticket);
                break;
            case RewardType.EpicTicketChest:
                shopManager.Reward10Pulls(ShopItemType.Ticket);
                break;
            case RewardType.Gems:
                shopManager.BuyGems(gems);
                break;
            case RewardType.Boost:
                shopManager.RewardSpecificBoost(boostData);
                break;
            case RewardType.MysteryBoost:
                shopManager.RewardMysteryBoost();
                break;
            case RewardType.MegaMysteryBoost:
                shopManager.RewardMegaMysteryBoost();
                break;
        }

    }

    private void UnlockEventMachine()
    {
        PlayerManager.instance.playerMachineData.currentEventMachineData.isUnlocked = true;
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

