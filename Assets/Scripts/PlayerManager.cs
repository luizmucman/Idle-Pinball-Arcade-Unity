using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public MachineManager currentMachine;
    public NumericalFormatter numFormat;

    // Player Settings
    public PlayerSettingsData playerSettingsData;

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
            playerCoins = 100000000;
            playerGems = 10000;
            if (ES3.KeyExists("playerManager"))
            {
                LoadPlayerData();

            }
            else
            {
                SceneManager.LoadScene("MA001");
            }
            DontDestroyOnLoad(this);
        }

        numFormat = new NumericalFormatter();
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

    public void AddCoins(ulong coins)
    {
        if (currentMachine.machineData.isCurrentEvent)
        {
            eventCoins += (ulong)(coins * playerTicketBuffs.coinBuff * UIManager.instance.uiBoostsManager.totalBoostAmt * currentMachine.paddleMultiplier);
            UIManager.instance.playerCoinText.text = numFormat.Format(eventCoins);
        }
        else
        {
            playerCoins += (ulong)(coins * playerTicketBuffs.coinBuff * UIManager.instance.uiBoostsManager.totalBoostAmt * currentMachine.paddleMultiplier);
            UIManager.instance.playerCoinText.text = numFormat.Format(playerCoins);
        }

    }

    public void RemoveCoins(ulong coins)
    {
        if (currentMachine.machineData.isCurrentEvent)
        {
            eventCoins -= (ulong)(coins * playerTicketBuffs.coinBuff * UIManager.instance.uiBoostsManager.totalBoostAmt * currentMachine.paddleMultiplier);
            UIManager.instance.playerCoinText.text = numFormat.Format(eventCoins);
        }
        else
        {
            playerCoins -= (ulong)(coins * playerTicketBuffs.coinBuff * UIManager.instance.uiBoostsManager.totalBoostAmt * currentMachine.paddleMultiplier);
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
                    machineData.accumulatedCoins += machineData.coinsPerSecond;
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
                    machineData.accumulatedCoins = (ulong)(machineData.coinsPerSecond * (3600 * maxIdleTime));
                }
                else
                {
                    machineData.accumulatedCoins = (ulong)(machineData.coinsPerSecond * idleLimitCheck.TotalSeconds);
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

    public void SavePlayerData()
    {
        ES3.Save("playerManager", gameObject);
        ES3.Save("boostDatabase", boostDatabase);

        foreach (MachineData machineData in mainMachines)
        {
            machineData.SaveMachine();
        }

        foreach (MachineData machineData in eventMachines)
        {
            machineData.SaveMachine();
        }

        seasonPassData.SaveSeasonPassData();
    }


    private void LoadPlayerData()
    {
        MachineData playedMachine = null;
        ES3.LoadInto<PlayerManager>("playerManager", gameObject);
        ES3.LoadInto<PlayerManager>("boostDatabase", boostDatabase);
        foreach (MachineData machineData in mainMachines)
        {
            machineData.LoadMachine();
            if (machineData.isPlaying)
            {
                playedMachine = machineData;
            }
        }

        foreach (MachineData machineData in eventMachines)
        {
            machineData.LoadMachine();
            if (machineData.isPlaying)
            {
                playedMachine = machineData;
            }

            if (machineData.isCurrentEvent)
            {
                currentEventMachineData = machineData;
            }
        }

        if (playedMachine != null)
        {
            SceneManager.LoadScene(playedMachine.machineGUID);
        }
        else
        {
            SceneManager.LoadScene("MA001");
        }

        seasonPassData.LoadSeasonPassData();
    }

    public void AddSeasonPassHit()
    {
        if(currentMachine.machineData.isCurrentEvent)
        {
            seasonPassData.AddSeasonPoints(1);
        }
    }
}
