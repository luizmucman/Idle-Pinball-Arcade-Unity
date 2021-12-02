using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using AppsFlyerSDK;
using UnityEngine.Purchasing.Security;
using TapjoyUnity;

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
    [SerializeField] public UIGatchaPopup ballGatchaPopup;
    [SerializeField] private UI10GatchaPopup ballTenGatchaPopup;
    [SerializeField] public UIGatchaPopup ticketGatchaPopup;
    [SerializeField] private UI10GatchaPopup ticketTenGatchaPopup;

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

    // Rewarded Gem Date;
    public DateTime lastRecordedDay;

    [Header("Boost Database")]
    // Booster Databases
    public List<BoostDatabase> mysteryBoostLists;
    public List<BoostDatabase> megaMysteryBoostLists;

    // Specials Buttons
    [SerializeField] private GameObject eventMachineProductObject;
    [SerializeField] private GameObject adFreeProductObject;
    [SerializeField] private GameObject starterPackObject;
    [SerializeField] private GameObject masterPackObject;

    [SerializeField] private string googleLicenseKey;

    private void Start()
    {
        lastRecordedDay = ES3.Load("gemDailyNextDate", new DateTime());
    }

    public void SendPurchaseToAppsFlyer(Product product)
    {
        string price = product.metadata.localizedPrice.ToString();
        string currency = product.metadata.isoCurrencyCode;

        string receipt = product.receipt;

        var recptToJSON = (Dictionary<string, object>)AFMiniJSON.Json.Deserialize(receipt);
        var receiptPayload = (Dictionary<string, object>)AFMiniJSON.Json.Deserialize((string)recptToJSON["Payload"]);

        #if UNITY_ANDROID && !UNITY_EDITOR 

        var purchaseData = (string)receiptPayload["json"];
        var signature = (string)receiptPayload["signature"];

        AppsFlyerAndroid.validateAndSendInAppPurchase(
        googleLicenseKey,
        signature,
        purchaseData,
        price,
        currency,
        null,
        this);

        #endif

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
        ES3.Save("playerIsAdFree", true);
        adFreeProductObject.SetActive(false);
        buyPopup.SetAdFreePopup();
    }

    public void Buy2xAllIncome()
    {
        PlayerManager.instance.is2xAllIncome = true;
        ES3.Save("playerHas2xIncome", true);
        buyPopup.SetIncomeBuffPopup();
    }

    public void Buy2xIdleIncome()
    {
        PlayerManager.instance.is2xIdleIncome = true;
        ES3.Save("playerHas2xIdleIncome", true);
        buyPopup.SetIdleBuffPopup();
    }

    public void Buy4xAllIncome()
    {
        PlayerManager.instance.is4xAllIncome = true;
        ES3.Save("playerHasMasterPack", true);
        buyPopup.SetTripleIncomeBuffPopup();
    }

    public void BuyStarterPack()
    {
        Buy2xAllIncome();
        PlayerManager.instance.AddGems(starterPackGems);
        starterPackObject.SetActive(false);
    }

    public void BuyMasterPack()
    {
        Buy4xAllIncome();
        PlayerManager.instance.AddGems(masterPackGems);
        masterPackObject.SetActive(false);
    }

    // Things that cost gems
    public void BuyCommonBallChest()
    {
        if(PlayerManager.instance.playerGems >= commonChestCost)
        {
            PlayerManager.instance.RemoveGems(commonChestCost);
            Reward1Pull(ShopItemType.Ball);
        }
    }

    public void BuyEpicBallChest()
    {
        if (PlayerManager.instance.playerGems >= legendaryChestCost)
        {
            PlayerManager.instance.RemoveGems(legendaryChestCost);
            Reward10Pulls(ShopItemType.Ball);
        }
    }

    public void BuyCommonTicketChest()
    {
        if (PlayerManager.instance.playerGems >= commonChestCost)
        {
            PlayerManager.instance.RemoveGems(commonChestCost);
            Reward1Pull(ShopItemType.Ticket);
        }
    }

    public void BuyEpicTicketChest()
    {
        if (PlayerManager.instance.playerGems >= legendaryChestCost)
        {
            PlayerManager.instance.RemoveGems(legendaryChestCost);
            Reward10Pulls(ShopItemType.Ticket);
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
        int rarityCheck = UnityEngine.Random.Range(0, 100);
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


        chosenBoost = GetBoostData(chosenDatabase.database[UnityEngine.Random.Range(0, chosenDatabase.database.Count)]);
        PlayerManager.instance.AddBoost(chosenBoost);
        ES3.Save("playerBoostInventory", PlayerManager.instance.boostInventory);
        buyPopup.SetPopup(chosenBoost);
    }

    public void RewardMegaMysteryBoost()
    {
        int rarityCheck = UnityEngine.Random.Range(0, 100);
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

        chosenBoost = GetBoostData(chosenDatabase.database[UnityEngine.Random.Range(0, chosenDatabase.database.Count)]);
        PlayerManager.instance.AddBoost(chosenBoost);
        ES3.Save("playerBoostInventory", PlayerManager.instance.boostInventory);
        buyPopup.SetPopup(chosenBoost);
    }

    //public void RewardCommonChest(ShopItemType itemType)
    //{
    //    List<Item> playerInventory;
    //    Item chosenItem;

    //    int chosenIndex = 0;
    //    int expAdded = 0;

    //    // Choosing the rank
    //    int randomRank = UnityEngine.Random.Range(0, 100);
    //    if (randomRank <= 20)
    //    {
    //        expAdded = 4;
    //    }
    //    else
    //    {
    //        expAdded = 1;
    //    }

    //    if (itemType == ShopItemType.Ball)
    //    {
    //        playerInventory = PlayerManager.instance.ballDatabase.database;
    //        chosenIndex = UnityEngine.Random.Range(1, PlayerManager.instance.ballDatabase.database.Count - 1);
    //        chosenItem = (Ball)PlayerManager.instance.ballDatabase.database[chosenIndex];
    //        PlayerManager.instance.ballDataList.GetItemData(chosenItem.GUID).AddExp(expAdded);
    //        UIManager.instance.uiBallManager.CheckUnlockedBalls();
    //    }
    //    else if(itemType == ShopItemType.Ticket)
    //    {
    //        playerInventory = PlayerManager.instance.ticketDatabase.database;
    //        chosenIndex = UnityEngine.Random.Range(0, PlayerManager.instance.ticketDatabase.database.Count - 1);
    //        chosenItem = (Ticket)PlayerManager.instance.ticketDatabase.database[chosenIndex];
    //        PlayerManager.instance.ticketDataList.GetItemData(chosenItem.GUID).AddExp(expAdded);
    //        UIManager.instance.uiTicketManager.CheckUnlockedTickets();
    //    }
    //    else
    //    {
    //        return;
    //    }



    //    chosenItem.itemData.isUnlocked = true;
    //    buyPopup.SetPopup(chosenItem);
    //}

    //public void RewardEpicChest(ShopItemType itemType)
    //{
    //    List<Item> playerInventory;
    //    Item chosenItem;

    //    int chosenIndex = 0;
    //    int expAdded = 0;

    //    // Choosing the rank
    //    int randomRank = UnityEngine.Random.Range(0, 100);
    //    if (randomRank <= 5)
    //    {
    //        expAdded = 40;
    //    }
    //    else
    //    {
    //        expAdded = 13;
    //    }


    //    if (itemType == ShopItemType.Ball)
    //    {
    //        playerInventory = PlayerManager.instance.ballDatabase.database;
    //        chosenIndex = UnityEngine.Random.Range(1, PlayerManager.instance.ballDatabase.database.Count - 1);
    //        chosenItem = (Ball)PlayerManager.instance.ballDatabase.database[chosenIndex];
    //        PlayerManager.instance.ballDataList.GetItemData(chosenItem.GUID).AddExp(expAdded);
    //        UIManager.instance.uiBallManager.CheckUnlockedBalls();
    //    }
    //    else if (itemType == ShopItemType.Ticket)
    //    {
    //        playerInventory = PlayerManager.instance.ticketDatabase.database;
    //        chosenIndex = UnityEngine.Random.Range(0, PlayerManager.instance.ticketDatabase.database.Count - 1);
    //        chosenItem = (Ticket)PlayerManager.instance.ticketDatabase.database[chosenIndex];
    //        PlayerManager.instance.ticketDataList.GetItemData(chosenItem.GUID).AddExp(expAdded);
    //        UIManager.instance.uiTicketManager.CheckUnlockedTickets();
    //    }
    //    else
    //    {
    //        return;
    //    }

    //    chosenItem.itemData.AddExp(expAdded);
    //    buyPopup.SetPopup(chosenItem);
    //}

    public void Reward1Pull(ShopItemType itemType)
    {
        ItemData chosenItemData;
        if (itemType == ShopItemType.Ball)
        {
            chosenItemData = PlayerManager.instance.ballDataList.GetRandomItem(ShopItemType.Ball);
            chosenItemData.AddExp(1);
            UIManager.instance.uiBallManager.CheckUnlockedBalls();
            ballGatchaPopup.SetPopup(chosenItemData);
        }
        else if (itemType == ShopItemType.Ticket)
        {
            chosenItemData = PlayerManager.instance.ticketDataList.GetRandomItem(ShopItemType.Ticket);
            chosenItemData.AddExp(1);
            UIManager.instance.uiTicketManager.CheckUnlockedTickets();
            ticketGatchaPopup.SetPopup(chosenItemData);
        }
        else
        {
            chosenItemData = null;
        }
    }

    public void Reward10Pulls(ShopItemType itemType)
    {
        List<ItemData> chosenItemDataList = new List<ItemData>();

        for (int i = 0; i < 10; i++)
        {
            ItemData chosenItemData;
            if (itemType == ShopItemType.Ball)
            {
                chosenItemData = PlayerManager.instance.ballDataList.GetRandomItem(ShopItemType.Ball);
                chosenItemData.AddExp(1);
                chosenItemDataList.Add(chosenItemData);
            }
            else if (itemType == ShopItemType.Ticket)
            {
                chosenItemData = PlayerManager.instance.ticketDataList.GetRandomItem(ShopItemType.Ticket);
                chosenItemData.AddExp(1);
                chosenItemDataList.Add(chosenItemData);
            }
        }

        if (itemType == ShopItemType.Ball)
        {
            UIManager.instance.uiBallManager.CheckUnlockedBalls();
            ballTenGatchaPopup.SetPopup(itemType, chosenItemDataList);
        }
        else if (itemType == ShopItemType.Ticket)
        {
            UIManager.instance.uiTicketManager.CheckUnlockedTickets();
            ticketTenGatchaPopup.SetPopup(itemType, chosenItemDataList);
        }

        
    }

    public void RewardSpecificBoost(BoostData boostData)
    {
        PlayerManager.instance.AddBoost(boostData);
        buyPopup.SetPopup(boostData);
        ES3.Save("playerBoostInventory", PlayerManager.instance.boostInventory);
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
        ES3.Save("SeasonPass-isPremium", true);
        seasonPassPopup.CloseWindow();
        eventMachineProductObject.SetActive(false);
        buyPopup.SetSeasonPassPopup();
    }

}
