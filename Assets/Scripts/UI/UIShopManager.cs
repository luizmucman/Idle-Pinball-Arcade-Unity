using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using AppsFlyerSDK;
using UnityEngine.Purchasing.Security;

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

    [SerializeField] private string googleLicenseKey;

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
        int expAdded = 0;

        // Choosing the rank
        int randomRank = Random.Range(0, 100);
        if (randomRank <= 20)
        {
            expAdded = 3;
        }
        else
        {
            expAdded = 1;
        }

        if (itemType == ShopItemType.Ball)
        {
            playerInventory = PlayerManager.instance.ballDatabase.database;
            chosenIndex = Random.Range(1, PlayerManager.instance.ballDatabase.database.Count - 1);
            chosenItem = (Ball)PlayerManager.instance.ballDatabase.database[chosenIndex];
            PlayerManager.instance.ballDataList.GetItemData(chosenItem.GUID).AddExp(expAdded);
            UIManager.instance.uiBallManager.CheckUnlockedBalls();
        }
        else if(itemType == ShopItemType.Ticket)
        {
            playerInventory = PlayerManager.instance.ticketDatabase.database;
            chosenIndex = Random.Range(0, PlayerManager.instance.ticketDatabase.database.Count - 1);
            chosenItem = (Ticket)PlayerManager.instance.ticketDatabase.database[chosenIndex];
            PlayerManager.instance.ticketDataList.GetItemData(chosenItem.GUID).AddExp(expAdded);
            UIManager.instance.uiTicketManager.CheckUnlockedTickets();
        }
        else
        {
            return;
        }



        chosenItem.itemData.isUnlocked = true;
        buyPopup.SetPopup(chosenItem);
    }

    public void RewardEpicChest(ShopItemType itemType)
    {
        List<Item> playerInventory;
        Item chosenItem;

        int chosenIndex = 0;
        int expAdded = 0;

        // Choosing the rank
        int randomRank = Random.Range(0, 100);
        if (randomRank <= legendaryChestEpicChance)
        {
            expAdded = 9;
        }
        else if (randomRank <= legendaryChestRareChance)
        {
            expAdded = 6;
        }
        else
        {
            expAdded = 3;
        }

        if (itemType == ShopItemType.Ball)
        {
            playerInventory = PlayerManager.instance.ballDatabase.database;
            chosenIndex = Random.Range(1, PlayerManager.instance.ballDatabase.database.Count - 1);
            chosenItem = (Ball)PlayerManager.instance.ballDatabase.database[chosenIndex];
            PlayerManager.instance.ballDataList.GetItemData(chosenItem.GUID).AddExp(expAdded);
        }
        else if (itemType == ShopItemType.Ticket)
        {
            playerInventory = PlayerManager.instance.ticketDatabase.database;
            chosenIndex = Random.Range(0, PlayerManager.instance.ticketDatabase.database.Count - 1);
            chosenItem = (Ticket)PlayerManager.instance.ticketDatabase.database[chosenIndex];
            PlayerManager.instance.ticketDataList.GetItemData(chosenItem.GUID).AddExp(expAdded);
        }
        else
        {
            return;
        }

        chosenItem.itemData.AddExp(expAdded);
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
