using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

[Serializable]
public class PlayerSave {
    public bool tutorialFinished;

    // Buffs
    public bool isAdFree;
    public bool is2xAllIncome;
    public bool is2xIdleIncome;
    public bool is4xAllIncome;

    // Event Data
    public ulong eventCoins;

    // Currency
    public ulong playerCoins;
    public int playerGems;

    // Tickets
    public int ticketSlotCount;

    // Boosts
    public List<BoostInUseSave> boostsInUse;
    public List<BoostOwnedSave> boostsOwned;

    // Balls

    // Tickets

    // Machine Data

    public PlayerSave (PlayerManager manager)
    {
        tutorialFinished = manager.tutorialFinished;
        isAdFree = manager.isAdFree;
        is2xAllIncome = manager.is2xAllIncome;
        is4xAllIncome = manager.is4xAllIncome;
        eventCoins = manager.eventCoins;
        playerCoins = manager.playerCoins;
        playerGems = manager.playerGems;

        ticketSlotCount = manager.ticketSlotCount;

        // Saving Boosts In Use
        List<BoostInUseSave> boostInUseList = new List<BoostInUseSave>();

        foreach(BoostData data in manager.boostDatabase.database)
        {
           boostInUseList.Add(data.SaveBoostDatabaseToJson());
        }

        boostsInUse = boostInUseList;

        // Save Boosts Owned
        List<BoostOwnedSave> boostsOwnedList = new List<BoostOwnedSave>();

        foreach(BoostData data in manager.boostInventory)
        {
            boostsOwnedList.Add(data.SaveBoostOwnedToJson());
        }

        boostsOwned = boostsOwnedList;

    }


}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    // PlayFab
    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;
    public bool facebookAccLinked;
    public bool googleAccLinked;

    public ES3Settings es3Cache;

    public MachineData currMachineData;
    public MachineManager currentMachine;
    public NumericalFormatter numFormat;
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
    public ulong eventCoins;
    public SeasonPassData seasonPassData;

    [Header("Global Stats")]
    public float globalCoinMultiplier;

    [Header("Boost Data")]
    public BoostDatabase boostDatabase;
    public List<BoostData> boostInventory;

    [Header("Player Currency")]
    // Player Currency
    public ulong playerCoins;
    public int playerGems;

    [Header("Player Inventories")]
    // Player Inventories
    public List<ItemData> ballInventory;
    public List<ItemData> ticketInventory;

    [Header("Player Ticket Data")]
    // Player Tickets
    public int ticketSlotCount;
    public List<ItemData> equippedTickets;
    public PlayerTicketBuffs playerTicketBuffs;

    [Header("Databases")]
    // Databases
    public ItemDatabase ballDatabase;
    public ItemDatabase ticketDatabase;

    [Header("Machine Data")]
    // Machine Data
    public List<MachineData> mainMachines;
    public List<MachineData> eventMachines;
    [HideInInspector] public MachineData currentEventMachineData;
    private float cpsSecondCounter;

    [Header("Away Data")]
    // Away Data
    public Double maxIdleTime;
    private DateTime timeAtPause;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            numFormat = new NumericalFormatter();
            soundsManager = GetComponent<SoundsManager>();
            es3Cache = new ES3Settings(ES3.Location.Cache);

            if (ES3.FileExists("SaveFile.es3"))
            {
                ES3.CacheFile("SaveFile.es3");
            }

            PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
            PlayFabAuthService.OnPlayFabError += OnPlayFabError;
            PlayFabAuthService.OnGoogleLink += OnGoogleLink;

            if (_AuthService.AuthType == 0)
            {
                _AuthService.Authenticate(Authtypes.Silent);
            }
            else
            {
                _AuthService.Authenticate();
            }

            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        cpsSecondCounter += Time.deltaTime;

        if(cpsSecondCounter >= 1)
        {
            AddMachineIdleCoins();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            timeAtPause = DateTime.Now;
            currentMachine.SetAwayTime();
            currentMachine.SaveMachine();
            SavePlayerData();
        }
        else
        {
            if(currentMachine != null)
            {
                AddMachineAwayCoins();
                currentMachine.RewardAway();
            }
        }
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
        //SavePlayerToJson();
    }

    private void Start()
    {
        soundsManager.PlayMusic("bgmMain");
        Application.targetFrameRate = 60;
    }

    public void AddCoins(ulong coins)
    {
        if (currentMachine.machineData.isCurrentEvent)
        {
            eventCoins += (ulong)(coins * playerTicketBuffs.coinBuff * UIManager.instance.uiBoostsManager.totalBoostAmt);
            UIManager.instance.playerCoinText.text = numFormat.Format(eventCoins);
        }
        else
        {
            playerCoins += (ulong)(coins * playerTicketBuffs.coinBuff * UIManager.instance.uiBoostsManager.totalBoostAmt);
            UIManager.instance.playerCoinText.text = numFormat.Format(playerCoins);
        }

    }

    public void RemoveCoins(ulong coins)
    {
        if (currentMachine.machineData.isCurrentEvent)
        {
            eventCoins -= (ulong)(coins);
            UIManager.instance.playerCoinText.text = numFormat.Format(eventCoins);
        }
        else
        {
            playerCoins -= (ulong)(coins);
            UIManager.instance.playerCoinText.text = numFormat.Format(playerCoins);
        }
    }

    public void AddGems(int gems)
    {
        playerGems += gems;
        UIManager.instance.playerGemText.text = playerGems.ToString();
    }

    public void RemoveGems(int gems)
    {
        playerGems -= gems;
        UIManager.instance.playerGemText.text = playerGems.ToString();
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
        }
    }

    public void UseBoost(BoostData boost)
    {
        BoostData boostRef = boostDatabase.GetBoost(boost.boostID);

        boostRef.AddTime(boost.boostLength);
    }

    private void AddMachineIdleCoins()
    {
        ChooseMachineListIdleCoins(mainMachines);
        ChooseMachineListIdleCoins(eventMachines);
    }

    private void ChooseMachineListIdleCoins(List<MachineData> machines)
    {
        foreach (MachineData machineData in machines)
        {
            if (machineData.isUnlocked && !machineData.isPlaying)
            {
                TimeSpan difference = DateTime.Now - machineData.awayCheckPoint;
                if (difference.TotalHours < maxIdleTime)
                {
                    machineData.accumulatedCoins += (ulong) (machineData.coinsPerSecond * playerTicketBuffs.idleCoinBuff);
                } 
            }
        }
    }

    private void AddMachineAwayCoins()
    {
        ChooseMachineListAwayCoins(mainMachines);
        ChooseMachineListAwayCoins(eventMachines);
    }

    private void ChooseMachineListAwayCoins(List<MachineData> machines)
    {
        foreach (MachineData machineData in machines)
        {
            if (machineData.isUnlocked)
            {
                TimeSpan idleLimitCheck = DateTime.Now - machineData.awayCheckPoint;
                if (idleLimitCheck.TotalHours > maxIdleTime)
                {
                    machineData.accumulatedCoins = (ulong)(machineData.coinsPerSecond * (3600 * (maxIdleTime + playerTicketBuffs.maxIdleTimeLength)) * playerTicketBuffs.idleCoinBuff);
                }
                else
                {
                    machineData.accumulatedCoins = (ulong)(machineData.coinsPerSecond * idleLimitCheck.TotalSeconds * playerTicketBuffs.idleCoinBuff);
                }
            }
        }
    }

    public MachineData GetEventMachineData(string machineGUID)
    {
        foreach (MachineData data in eventMachines)
        {
            if (data.machineGUID.Equals(machineGUID))
            {
                return data;
            }
        }
        return null;
    }

    public void SetTicketBuffs()
    {
        foreach(ItemData ticketData in equippedTickets)
        {
            Ticket currTicket = (Ticket) ticketDatabase.GetItem(ticketData.GUID);

            currTicket.EquipTicket();
        }
    }

    public ItemData CheckIfBallOwned(string currGUID)
    {
        foreach (ItemData currData in ballInventory)
        {
            if (currGUID.Equals(currData.GUID))
            {
                return currData;
            }
        }
        return null;
    }

    public void CollectAllIdleCoins(Double multiplier)
    {
        foreach (MachineData machineData in mainMachines)
        {
            if (machineData.isUnlocked && !machineData.isPlaying)
            {
                AddCoins((ulong) (machineData.accumulatedCoins * multiplier));
                machineData.accumulatedCoins = 0;
            }
        }

        foreach (MachineData machineData in eventMachines)
        {
            if (machineData.isUnlocked && !machineData.isPlaying)
            {
                AddCoins((ulong) (machineData.accumulatedCoins * multiplier));
                machineData.accumulatedCoins = 0;
            }
        }
    }

    public void AddSeasonPassHit()
    {
        if (currentMachine.machineData.isCurrentEvent)
        {
            seasonPassData.AddSeasonPoints(1);
        }
    }

    public void SavePlayerData()
    {
        SavePlayerManager();
        boostDatabase.SaveBoostDatabase();
        SaveMachineData();

        seasonPassData.SaveSeasonPassData();

        ES3.StoreCachedFile();
        SaveToPlayFab();
    }

    private void SaveToPlayFab()
    {
        string rawSave = ES3.LoadRawString(es3Cache);

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"es3Save", rawSave}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private void LoadPlayerData()
    {
        LoadFromPlayfab();

    }

    public void LoadFromCache()
    {
        if(ES3.KeyExists("playerCoins"))
        {
            LoadPlayerManager();
            boostDatabase.LoadBoostDatabase();
            LoadMachineData();

            seasonPassData.LoadSeasonPassData();

            AddMachineAwayCoins();
        }
        else
        {
            SceneManager.LoadScene("MA001");
        }

    }

    private void SaveMachineData()
    {
        foreach (MachineData machineData in mainMachines)
        {
            machineData.SaveMachine();
        }

        foreach (MachineData machineData in eventMachines)
        {
            machineData.SaveMachine();
        }
    }

    private void LoadMachineData()
    {
        currMachineData = null;
        foreach (MachineData machineData in mainMachines)
        {
            machineData.LoadMachine();
            if (machineData.isPlaying)
            {
                currMachineData = machineData;
            }
        }

        foreach (MachineData machineData in eventMachines)
        {
            machineData.LoadMachine();
            if (machineData.isPlaying)
            {
                currMachineData = machineData;
            }

            if (machineData.isCurrentEvent)
            {
                currentEventMachineData = machineData;
            }
        }

        if (currMachineData != null)
        {
            SceneManager.LoadScene(currMachineData.machineGUID);
        }
        else
        {
            SceneManager.LoadScene("MA001");
        }
    }

    private void SavePlayerManager()
    {
        ES3.Save("playerTutorialFinished", tutorialFinished);
        ES3.Save("playerIsAdFree", isAdFree);
        ES3.Save("playerHas2xIncome", is2xAllIncome);
        ES3.Save("playerHas2xIdleIncome", is2xIdleIncome);

        ES3.Save("playerGlobalCoinMultiplier", globalCoinMultiplier);
        ES3.Save("playerBoostDatabase", boostDatabase);
        ES3.Save("playerBoostInventory", boostInventory);

        ES3.Save("playerCoins", playerCoins);
        ES3.Save("playerGems", playerGems);
        ES3.Save("playerBallInventory", ballInventory);
        ES3.Save("playerTicketInventory", ticketInventory);
        ES3.Save("playerTicketSlotCount", ticketSlotCount);
        ES3.Save("playerEquippedTickets", equippedTickets);
    }

    private void LoadPlayerManager()
    {
        tutorialFinished = ES3.Load("playerTutorialFinished", tutorialFinished);
        isAdFree = ES3.Load("playerIsAdFree", isAdFree);
        is2xAllIncome = ES3.Load("playerHas2xIncome", is2xAllIncome);
        is2xIdleIncome = ES3.Load("playerHas2xIdleIncome", is2xIdleIncome);

        globalCoinMultiplier = ES3.Load("playerGlobalCoinMultiplier", globalCoinMultiplier);
        boostDatabase = ES3.Load("playerBoostDatabase", boostDatabase);
        boostInventory = ES3.Load("playerBoostInventory", boostInventory);

        playerCoins = ES3.Load("playerCoins", playerCoins);
        playerGems = ES3.Load("playerGems", playerGems);
        ballInventory = ES3.Load("playerBallInventory", ballInventory);
        ticketInventory = ES3.Load("playerTicketInventory", ticketInventory);
        ticketSlotCount = ES3.Load("playerTicketSlotCount", ticketSlotCount);
        equippedTickets = ES3.Load("playerEquippedTickets", equippedTickets);
    }

    // Auth Dependency

    private void OnGoogleLink(LinkGoogleAccountResult success)
    {
        throw new NotImplementedException();
    }

    private void OnPlayFabError(PlayFabError error)
    {
        Debug.Log(error);
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
            ES3.SaveRaw(rawString, es3Cache);
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
    }
}
