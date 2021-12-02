using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMachineData : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private double totalPlayerCoins;

    [Header("Machine Data")]
    // Machine Data
    public List<MachineData> mainMachines;
    public List<MachineData> eventMachines;
    public MachineData currentEventMachineData;
    public List<MachineData> ownedMachines;
    [SerializeField] private float cpsSecondCounter = 0.0f;
    private float coinSaveCounter = 0.0f;

    // Current Machine
    public MachineData currMachineData;
    public MachineManager currMachine;

    [Header("Away Data")]
    // Away Data
    [SerializeField] public double maxIdleTime;
    [SerializeField] private double awayCoinMultiplier;

    private void Start()
    {
        PlayerManager.instance.DividePlayerCoins();
    }

    //private DateTime timeAtPause;

    // Update is called once per frame
    void Update()
    {
        cpsSecondCounter += Time.deltaTime;
        coinSaveCounter += Time.deltaTime;

        if (cpsSecondCounter >= 1)
        {
            //AddMachineIdleCoins();
            PopulateTotalCoins();
  
            cpsSecondCounter = 0;
        }


    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //timeAtPause = DateTime.Now;
            currMachineData.SetAwayCheckpoint();
            currMachineData.SaveMachine();
            //currMachine.SaveMachine();
        }
        else
        {
            if (currMachine != null)
            {
                //AddMachineAwayCoins();
                currMachine.RewardAway();
            }
        }
    }

    // Coin Methods
    public void AddCoinsToCurrentMachine(double coins) 
    {
        currMachineData.AddCoins(coins);
    }

    public void RemoveCoinsFromCurrentMachine(double coins)
    {
        currMachineData.RemoveCoins(coins);
    }

    // Idle and Away Methods
    //public void AddMachineIdleCoins()
    //{
    //    ChooseMachineListIdleCoins(mainMachines);
    //    ChooseMachineListIdleCoins(eventMachines);
    //}

    //private void ChooseMachineListIdleCoins(List<MachineData> machines)
    //{
    //    foreach (MachineData machineData in machines)
    //    {
    //        if ((machineData.isUnlocked || machineData.isCurrentEvent) && !machineData.isPlaying)
    //        {
    //             machineData.AddCoins((double)(machineData.coinsPerSecond * playerManager.playerTicketBuffs.idleCoinBuff));
    //        }
    //    }
    //}

    //public void AddMachineAwayCoins()
    //{
    //    ChooseMachineListAwayCoins(mainMachines);
    //    ChooseMachineListAwayCoins(eventMachines);
    //}

    //private void ChooseMachineListAwayCoins(List<MachineData> machines)
    //{
    //    foreach (MachineData machineData in machines)
    //    {
    //        if (machineData.isUnlocked)
    //        {
    //            TimeSpan idleLimitCheck = DateTime.Now - machineData.awayCheckPoint;
    //            if (idleLimitCheck.TotalHours > maxIdleTime)
    //            {
    //                machineData.accumulatedCoins = (double)((machineData.coinsPerSecond * (3600 * (maxIdleTime + PlayerManager.instance.playerTicketBuffs.maxIdleTimeLength)) * PlayerManager.instance.playerTicketBuffs.idleCoinBuff) * awayCoinMultiplier);
    //            }
    //            else
    //            {
    //                machineData.accumulatedCoins = (double)((machineData.coinsPerSecond * idleLimitCheck.TotalSeconds * PlayerManager.instance.playerTicketBuffs.idleCoinBuff) * awayCoinMultiplier);
    //            }
    //        }
    //    }
    //}

    //public void CollectAllIdleCoins(double multiplier)
    //{
    //    foreach (MachineData machineData in mainMachines)
    //    {
    //        if (machineData.isUnlocked && !machineData.isPlaying)
    //        {
    //            currMachineData.AddCoins((double)(machineData.accumulatedCoins * multiplier));
    //            machineData.accumulatedCoins = 0;
    //        }
    //    }

    //    foreach (MachineData machineData in eventMachines)
    //    {
    //        if (machineData.isUnlocked && !machineData.isPlaying)
    //        {
    //            currMachineData.AddCoins((double)(machineData.accumulatedCoins * multiplier));
    //            machineData.accumulatedCoins = 0;
    //        }
    //    }
    //}

    // Internal Methods

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

    public bool IsPlayingEvent()
    {
        return currMachineData.isCurrentEvent;
    }

    private void PopulateTotalCoins()
    {
        double currCoins = 0;

        foreach (MachineData machine in mainMachines)
        {
            currCoins += machine.GetCoinCount();

        }


        foreach (MachineData machine in eventMachines)
        {
            currCoins += machine.GetCoinCount();

        }

        if (coinSaveCounter >= 10)
        {
            coinSaveCounter = 0;
            currMachine.SaveMachine();
        }

        totalPlayerCoins = currCoins;

        UIManager.instance.uiMachineManager.SetTotalCoinText(totalPlayerCoins);
    }

    public double GetTotalPlayerCoins()
    {
        return totalPlayerCoins;
    }

    public bool SpendTotalPlayerCoins(double cost)
    {
        double currCost = cost;

        if (currCost <= totalPlayerCoins)
        {
            foreach (MachineData data in mainMachines)
            {
                double machineCoins = data.GetCoinCount();
                if (machineCoins < currCost)
                {
                    currCost -= machineCoins;
                    data.RemoveCoins(machineCoins);
                }
                else
                {
                    data.RemoveCoins(currCost);
                    currCost = 0;
                    break;
                }
            }

            foreach (MachineData data in eventMachines)
            {
                double machineCoins = data.GetCoinCount();
                if (machineCoins < currCost)
                {
                    currCost -= machineCoins;
                    data.RemoveCoins(machineCoins);
                }
                else
                {
                    data.RemoveCoins(currCost);
                    currCost = 0;
                    break;
                }
            }

            return true;
        }

        return false;
    }

    // Save and Load Methods

    //public void SetNewGame()
    //{
    //    currMachineData = mainMachines[0];
    //    foreach (MachineData machineData in eventMachines)
    //    {
    //        if (machineData.isCurrentEvent)
    //        {
    //            currentEventMachineData = machineData;
    //        }
    //    }
    //    SceneManager.LoadScene("MA001");
    //}

    public void SaveMachineData()
    {
        //timeAtPause = DateTime.Now;
        //currMachine.SetAwayTime();

        //currMachine.SaveMachine();

        //foreach (MachineData machineData in mainMachines)
        //{
        //    machineData.SaveMachine();
        //}

        //foreach (MachineData machineData in eventMachines)
        //{
        //    machineData.SaveMachine();
        //}
    }

    public void LoadMachineData()
    {
        currMachineData = null;
        foreach (MachineData machineData in mainMachines)
        {
            machineData.LoadMachine();
            if (machineData.isPlaying)
            {
                currMachineData = machineData;
            }

            if (machineData.isUnlocked)
            {
                ownedMachines.Add(machineData);
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

            if (machineData.isUnlocked)
            {
                ownedMachines.Add(machineData);
            }
        }

    }

    public void InitialSceneLoad()
    {
        if (currMachineData != null)
        {
            SceneManager.LoadScene(currMachineData.machineGUID);
        }
        else
        {
            currMachineData = mainMachines[0];
            SceneManager.LoadScene("MA001");
        }
    }
}
