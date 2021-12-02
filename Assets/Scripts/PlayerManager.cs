using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private bool finishedLoadMethod;

    // PlayFab Saves
    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;
    public string playFabID;
    public bool facebookAccLinked;
    public bool googleAccLinked;
    //public ES3Settings es3Cache;

    // Current Machine
    public PlayerMachineData playerMachineData;

    // Connected Components
    public SoundsManager soundsManager;

    // Player Settings
    public PlayerSettingsData playerSettingsData;

    // Tutorial Data
    public bool tutorialFinished;

    // Player Purchases
    public bool isAdFree;
    public bool is2xAllIncome;
    public bool is2xIdleIncome;
    public bool is4xAllIncome;

    [Header("SeasonPassData")]
    public double eventCoins;
    public SeasonPassData seasonPassData;

    [Header("Boost Data")]
    public BoostDatabase boostDatabase;
    public List<BoostData> boostInventory;

    [Header("Player Currency")]
    // Player Currency
    public double playerCoins;
    public int playerGems;

    [Header("Player Ticket Data")]
    // Player Tickets
    public int ticketSlotCount;
    public List<ItemData> equippedTickets;
    public PlayerTicketBuffs playerTicketBuffs;

    [Header("Databases")]
    // Databases
    public ItemDatabase ballDatabase;
    public ItemDatabase ticketDatabase;

    // Item Data
    public ItemDataList ballDataList;
    public ItemDataList ticketDataList;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            GetComponent<SDKInit>().InitSDK();
            IronSource.Agent.init("1206944e5");
            PopulateItemLists();
            //es3Cache = new ES3Settings(ES3.Location.Cache);

            //if (ES3.FileExists("SaveFile.es3"))
            //{
            //    ES3.CacheFile("SaveFile.es3");
            //}

            PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
            PlayFabAuthService.OnPlayFabError += OnPlayFabError;

            _AuthService.Authenticate(Authtypes.Silent);


            DontDestroyOnLoad(this);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        IronSource.Agent.onApplicationPause(pause);
        if (pause)
        {
            Debug.Log("Pausing");
            SavePlayerData();
        }
        else
        {

        }
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public double AddCoins(double coins)
    {
        double coinGain = (double)(coins * playerTicketBuffs.coinBuff * UIManager.instance.uiBoostsManager.totalBoostAmt);
        //if (currentMachine.machineData.isCurrentEvent)
        //{
        //    eventCoins += coinGain;
        //    UIManager.instance.playerCoinText.text = DoubleFormatter.Format(eventCoins);
        //}
        //else
        //{
        //    playerCoins += coinGain;
        //    UIManager.instance.playerCoinText.text = DoubleFormatter.Format(playerCoins);
        //}

        playerMachineData.AddCoinsToCurrentMachine(coinGain);
        UIManager.instance.playerCoinText.text = DoubleFormatter.Format(playerMachineData.currMachineData.GetCoinCount());
        return coinGain;
    }

    public void RemoveCoins(double coins)
    {
        //if (currentMachine.machineData.isCurrentEvent)
        //{
        //    eventCoins -= (double)(coins);
        //    UIManager.instance.playerCoinText.text = DoubleFormatter.Format(eventCoins);
        //}
        //else
        //{
        //    playerCoins -= (double)(coins);
        //    UIManager.instance.playerCoinText.text = DoubleFormatter.Format(playerCoins);
        //}

        playerMachineData.RemoveCoinsFromCurrentMachine(coins);
    }

    public void AddGems(int gems)
    {
        playerGems += gems;
        UIManager.instance.playerGemText.text = playerGems.ToString();
        ES3.Save("playerGems", playerGems);
    }

    public void RemoveGems(int gems)
    {
        playerGems -= gems;
        UIManager.instance.playerGemText.text = playerGems.ToString();
        ES3.Save("playerGems", playerGems);
    }

    public void AddTicketSlot()
    {
        ticketSlotCount++;
        ES3.Save("playerTicketSlotCount", ticketSlotCount);
    }

    public void AddBoost(BoostData boost)
    {
        bool isOwned = false;

        foreach(BoostData ownedBoost in boostInventory)
        {
            if(boost.boostID.Equals(ownedBoost.boostID) && boost.boostLength == ownedBoost.boostLength)
            {
                ownedBoost.qtyOwned++;
                isOwned = true;
                break;
            }
        }

        if(!isOwned)
        {
            boost.qtyOwned = 1;
            boostInventory.Add(boost);
            UIManager.instance.uiBoostsManager.AddNewOwnedBoostContainer(boost);
        }
    }

    public void UseBoost(BoostData boost)
    {
        BoostData boostRef = boostDatabase.GetBoost(boost.boostID);

        boostRef.AddTime(boost.boostLength);
    }

    public void SetTicketBuffs()
    {
        foreach(ItemData ticketData in equippedTickets)
        {
            Ticket currTicket = (Ticket) ticketDatabase.GetItem(ticketData.GUID);

            currTicket.EquipTicket();
        }
    }

    public void AddSeasonPassHit()
    {
        if (playerMachineData.IsPlayingEvent())
        {
            seasonPassData.CheckSeasonPassProgress();
        }
    }

    private void PopulateItemLists()
    {
        ballDataList = new ItemDataList();
        foreach (Item ball in ballDatabase.database)
        {
            ballDataList.AddNewItemData(ball.GUID);
        }

        ticketDataList = new ItemDataList();
        foreach (Item ticket in ticketDatabase.database)
        {
            ticketDataList.AddNewItemData(ticket.GUID);
        }
    }

    public void SavePlayerData()
    {
        if (finishedLoadMethod)
        {
            //SavePlayerManager();
            //playerSettingsData.Save();
            //playerMachineData.SaveMachineData();


            seasonPassData.SaveSeasonPassData();
            UIManager.instance.uiChallengeManager.SaveChallenges();

            //ES3.StoreCachedFile();
            SaveToPlayFab();
        }

    }

    private void SaveToPlayFab()
    {
        //string rawSave = ES3.LoadRawString(es3Cache);

        string str = ES3.LoadRawString("SaveFile.es3");


        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"es3Save", str}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void LoadFromCache()
    {

        LoadPlayerManager();
        playerSettingsData.Load();
        playerMachineData.LoadMachineData();
        seasonPassData.LoadSeasonPassData();

        //playerMachineData.AddMachineAwayCoins();

        finishedLoadMethod = true;

        playerMachineData.InitialSceneLoad();
    }

    private void SavePlayerManager()
    {
        //ES3.Save("playerTutorialFinished", tutorialFinished);
        //ES3.Save("playerIsAdFree", isAdFree);
        //ES3.Save("playerHas2xIncome", is2xAllIncome);
        //ES3.Save("playerHas2xIdleIncome", is2xIdleIncome);
        //ES3.Save("playerHasMasterPack", is4xAllIncome);
        //boostDatabase.SaveBoostDatabase();
        //ES3.Save("playerBoostInventory", boostInventory);
        //ES3.Save("playerGems", playerGems);
        //ticketDataList.SaveItemList();
        //ballDataList.SaveItemList();
        //ES3.Save("playerTicketSlotCount", ticketSlotCount);

        //ES3.Save("playerEventCoins", eventCoins);
        //ES3.Save("playerCoins", playerCoins);





        //ES3.Save("playerEquippedTickets", equippedTickets);
    }

    private void LoadPlayerManager()
    {
        tutorialFinished = ES3.Load("playerTutorialFinished", tutorialFinished);
        isAdFree = ES3.Load("playerIsAdFree", isAdFree);
        is2xAllIncome = ES3.Load("playerHas2xIncome", is2xAllIncome);
        is2xIdleIncome = ES3.Load("playerHas2xIdleIncome", is2xIdleIncome);
        is4xAllIncome = ES3.Load("playerHasMasterPack", is4xAllIncome);

        boostDatabase.LoadBoostDatabase();
        boostInventory = ES3.Load("playerBoostInventory", boostInventory);

        eventCoins = ES3.Load("playerEventCoins", eventCoins);
        playerCoins = ES3.Load("playerCoins", playerCoins);
        playerGems = ES3.Load("playerGems", playerGems);

        ticketDataList.LoadItemList();
        ballDataList.LoadItemList();

        ticketSlotCount = ES3.Load("playerTicketSlotCount", ticketSlotCount);
        equippedTickets = ES3.Load("playerEquippedTickets", equippedTickets);
    }

    public void DividePlayerCoins()
    {
        if(playerCoins > 100)
        {
            foreach(MachineData machineData in playerMachineData.ownedMachines)
            {
                machineData.AddCoins(playerCoins / playerMachineData.ownedMachines.Count);
            }

            playerCoins = 0;

        }
    }

    // Auth Dependency

    private void OnGoogleLink(LinkGoogleAccountResult success)
    {
        throw new NotImplementedException();
    }

    private void OnPlayFabError(PlayFabError error)
    {
        Debug.Log(error);
        LoadFromCache();
    }

    private void OnLoginSuccess(LoginResult success)
    {
        if(success.InfoResultPayload.AccountInfo.FacebookInfo != null)
        {
            facebookAccLinked = true;
        }
        if(success.InfoResultPayload.AccountInfo.GoogleInfo != null)
        {
            googleAccLinked = true;
        }

        playFabID = success.PlayFabId;

        LoadFromCache();
    }


    // Loading From Playfab

    public void LoadFromPlayfab()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("es3Save"))
        {
            string rawString = result.Data["es3Save"].Value;
            ES3.SaveRaw(rawString, "SaveFile.es3");
            LoadFromCache();
        }
        else 
        {
            LoadFromCache();
        }
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Success user data send");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error);
        LoadFromCache();
    }
}
