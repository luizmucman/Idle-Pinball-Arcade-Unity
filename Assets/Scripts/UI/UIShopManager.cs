using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShopItemType
{
    Ticket,
    Ball
}

public class UIShopManager : MonoBehaviour
{
    [Header("Pack Gem Rewards")]
    public int starterPackGems;
    public int masterPackGems;

    [Header("UI Popups")]
    // UI Popups
    public UIShopPopup buyPopup;
    public UISeasonPassPopup seasonPassPopup;

    [Header("Chest Chances")]
    // Chances
    public int commonChestGreatChance;
    public int legendaryChestEpicChance;
    public int legendaryChestRareChance;

    [Header("Gem Costs")]
    // Gem Costs
    public int commonChestCost;
    public int legendaryChestCost;
    public int mysteryBoostCost;
    public int megaMysteryBoostCost;

    [Header("Boost Database")]
    // Booster Databases
    public List<BoostDatabase> mysteryBoostLists;
    public List<BoostDatabase> megaMysteryBoostLists;


    // Specials Buttons
    public Button adFreeButton;
    public Button incomeBuffButton;
    public Button idleBuffButton;

    private void Start()
    {
        if (PlayerManager.instance.isAdFree)
        {
            adFreeButton.enabled = false;
            adFreeButton.GetComponentInChildren<Text>().text = "OWNED";
        }
        if (PlayerManager.instance.is2xAllIncome)
        {
            incomeBuffButton.enabled = false;
            incomeBuffButton.GetComponentInChildren<Text>().text = "OWNED";
        }
        if (PlayerManager.instance.is2xIdleIncome)
        {
            idleBuffButton.enabled = false;
            idleBuffButton.GetComponentInChildren<Text>().text = "OWNED";
        }
    }

    // Things that cost money
    public void BuyGems(int amount)
    {
        PlayerManager.instance.AddGems(amount);
        buyPopup.SetGemPopup(amount);
    }

    public void BuyAdFree()
    {
        PlayerManager.instance.isAdFree = true;
        buyPopup.SetAdFreePopup();
    }

    public void Buy2xAllIncome()
    {
        PlayerManager.instance.is2xAllIncome = true;
        buyPopup.SetIncomeBuffPopup();
    }

    public void Buy2xIdleIncome()
    {
        PlayerManager.instance.is2xIdleIncome = true;
        buyPopup.SetIdleBuffPopup();
    }

    public void Buy4xAllIncome()
    {
        PlayerManager.instance.is4xAllIncome = true;
        buyPopup.SetTripleIncomeBuffPopup();
    }

    public void BuyStarterPack()
    {
        Buy2xAllIncome();
        PlayerManager.instance.AddGems(starterPackGems);
    }

    public void BuyMasterPack()
    {
        Buy4xAllIncome();
        PlayerManager.instance.AddGems(masterPackGems);
    }

    // Things that cost gems
    public void BuyCommonBallChest()
    {
        if(PlayerManager.instance.playerGems >= commonChestCost)
        {
            PlayerManager.instance.RemoveGems(commonChestCost);
            RewardCommonChest(ShopItemType.Ball);
        }
    }

    public void BuyEpicBallChest()
    {
        if (PlayerManager.instance.playerGems >= legendaryChestCost)
        {
            PlayerManager.instance.RemoveGems(legendaryChestCost);
            RewardEpicChest(ShopItemType.Ball);
        }
    }

    public void BuyCommonTicketChest()
    {
        if (PlayerManager.instance.playerGems >= commonChestCost)
        {
            PlayerManager.instance.RemoveGems(commonChestCost);
            RewardCommonChest(ShopItemType.Ticket);
        }
    }

    public void BuyEpicTicketChest()
    {
        if (PlayerManager.instance.playerGems >= legendaryChestCost)
        {
            PlayerManager.instance.RemoveGems(legendaryChestCost);
            RewardEpicChest(ShopItemType.Ticket);
        }
    }

    public void BuyMysteryBoost()
    {
        if (PlayerManager.instance.playerGems >= mysteryBoostCost)
        {
            PlayerManager.instance.RemoveGems(mysteryBoostCost);
            RewardMysteryBoost();
        }
    }

    public void BuyMegaMysteryBoost()
    {
        if (PlayerManager.instance.playerGems >= megaMysteryBoostCost)
        {
            PlayerManager.instance.RemoveGems(megaMysteryBoostCost);
            RewardMegaMysteryBoost();
        }
    }


    // Rewarding items
    public void RewardMysteryBoost()
    {
        int rarityCheck = Random.Range(0, 100);
        BoostData chosenBoost;
        BoostDatabase chosenDatabase;

        if (rarityCheck <= 80)
        {
            chosenDatabase = mysteryBoostLists[0];
        }
        else if (rarityCheck <= 98)
        {
            chosenDatabase = mysteryBoostLists[1];
        }
        else
        {
            chosenDatabase = mysteryBoostLists[2];
        }


        chosenBoost = GetBoostData(chosenDatabase.database[Random.Range(0, chosenDatabase.database.Count)]);
        PlayerManager.instance.AddBoost(chosenBoost);

        buyPopup.SetPopup(chosenBoost);
    }

    public void RewardMegaMysteryBoost()
    {
        int rarityCheck = Random.Range(0, 100);
        BoostData chosenBoost;
        BoostDatabase chosenDatabase;

        if (rarityCheck <= 80)
        {
            chosenDatabase = megaMysteryBoostLists[0];
        }
        else if (rarityCheck <= 98)
        {
            chosenDatabase = megaMysteryBoostLists[1];
        }
        else
        {
            chosenDatabase = megaMysteryBoostLists[2];
        }

        chosenBoost = GetBoostData(chosenDatabase.database[Random.Range(0, chosenDatabase.database.Count)]);
        PlayerManager.instance.AddBoost(chosenBoost);

        buyPopup.SetPopup(chosenBoost);
    }

    public void RewardCommonChest(ShopItemType itemType)
    {
        List<Item> playerInventory;
        Item chosenItem;

        int chosenIndex = 0;

        if (itemType == ShopItemType.Ball)
        {
            playerInventory = PlayerManager.instance.ballDatabase.database;
            chosenIndex = Random.Range(1, PlayerManager.instance.ballDatabase.database.Count - 1);
            chosenItem = (Ball)PlayerManager.instance.ballDatabase.database[chosenIndex];
        }
        else if(itemType == ShopItemType.Ticket)
        {
            playerInventory = PlayerManager.instance.ticketDatabase.database;
            chosenIndex = Random.Range(0, PlayerManager.instance.ticketDatabase.database.Count - 1);
            chosenItem = (Ticket)PlayerManager.instance.ticketDatabase.database[chosenIndex];
        }
        else
        {
            return;
        }

        // Choosing the rank
        int randomRank = Random.Range(0, 100);
        if (randomRank <= 20)
        {
            chosenItem.itemData.AddExp(3);
        }
        else
        {
            chosenItem.itemData.AddExp(1);
        }

        chosenItem.itemData.isUnlocked = true;
        buyPopup.SetPopup(chosenItem);
    }

    public void RewardEpicChest(ShopItemType itemType)
    {
        List<Item> playerInventory;
        Item chosenItem;

        int chosenIndex = 0;

        if (itemType == ShopItemType.Ball)
        {
            playerInventory = PlayerManager.instance.ballDatabase.database;
            chosenIndex = Random.Range(1, PlayerManager.instance.ballDatabase.database.Count - 1);
            chosenItem = (Ball)PlayerManager.instance.ballDatabase.database[chosenIndex];
        }
        else if (itemType == ShopItemType.Ticket)
        {
            playerInventory = PlayerManager.instance.ticketDatabase.database;
            chosenIndex = Random.Range(0, PlayerManager.instance.ticketDatabase.database.Count - 1);
            chosenItem = (Ticket)PlayerManager.instance.ticketDatabase.database[chosenIndex];
        }
        else
        {
            return;
        }

        // Choosing the rank
        int randomRank = Random.Range(0, 100);
        if (randomRank <= legendaryChestEpicChance)
        {
            chosenItem.itemData.AddExp(9);
        }
        else if (randomRank <= legendaryChestRareChance)
        {
            chosenItem.itemData.AddExp(6);
        }
        else
        {
            chosenItem.itemData.AddExp(3);
        }

        chosenItem.itemData.isUnlocked = true;
        buyPopup.SetPopup(chosenItem);
    }

    private BoostData GetBoostData(BoostData data)
    {
        BoostData currBoostData = new BoostData();
        currBoostData.boostID = data.boostID;
        currBoostData.boostAmt = data.boostAmt;
        currBoostData.boostLength = data.boostLength;

        return currBoostData;
    }

    public void RewardPremiumSeasonPass()
    {

        PlayerManager.instance.seasonPassData.isPremium = true;
    }
}
