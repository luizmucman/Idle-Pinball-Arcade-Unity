using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

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
            if (ES3.KeyExists("playerCoins"))
            {
                LoadPlayerData();
            }
            else
            {
                currMachineData = mainMachines[0];
                SceneManager.LoadScene("MA001");
            }
            DontDestroyOnLoad(this);
        }

        numFormat = new NumericalFormatter();
        soundsManager = GetComponent<SoundsManager>();
        //playerTicketBuffs = new PlayerTicketBuffs();
        AddMachineAwayCoins();

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
    }

    private void Start()
    {
        soundsManager.PlayMusic("bgmMain");
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
    }

    private void LoadPlayerData()
    {
        LoadPlayerManager();
        boostDatabase.LoadBoostDatabase();
        LoadMachineData();

        seasonPassData.LoadSeasonPassData();
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
        if(ES3.KeyExists("playerCoins"))
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
    }
}
