using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MachineData
{
    // Machine Coins
    [SerializeField] private double machineCoins;

    // Machine Data
    public string machineGUID;
    public string machineName;
    public double machineCost;
    public bool isUnlocked;
    public bool isPlaying;
    public bool isEvent;
    public bool isCurrentEvent;

    // Total stats
    public double totalCoinsGained;

    // Idle coin stats
    public double coinsPerSecond;
    //public double accumulatedCoins;

    // UI Data
    public Sprite machineImage;
    public Sprite machineCoinImage;

    // Away Data
    public DateTime awayCheckPoint;

    public void AddCoins(double coins)
    {
        machineCoins += coins;

        if(PlayerManager.instance.playerMachineData.currMachineData == this)
        {
            UIManager.instance.playerCoinText.text = DoubleFormatter.Format(GetCoinCount());
        }

    }

    public void RemoveCoins(double coins)
    {
        machineCoins -= coins;

        if (PlayerManager.instance.playerMachineData.currMachineData == this)
        {
            UIManager.instance.playerCoinText.text = DoubleFormatter.Format(GetCoinCount());
        }

        SaveMachine();
    }

    public double GetCoinCount()
    {
        return machineCoins;
    }

    public void SetAwayCheckpoint()
    {
        awayCheckPoint = DateTime.Now;
        ES3.Save(machineGUID + "-awayCheckPoint", awayCheckPoint);
    }

    public void SetCPS(double cps)
    {
        coinsPerSecond = cps;
        ES3.Save(machineGUID + "-coinsPerSecond", coinsPerSecond);
    }



    public void SaveMachine()
    {
        ES3.Save(machineGUID + "-machineCoins", machineCoins);
        //ES3.Save(machineGUID + "-coinsPerSecond", coinsPerSecond);
        //ES3.Save(machineGUID + "-accumulatedCoins", accumulatedCoins);
        ES3.Save(machineGUID + "-totalCoinsGained", totalCoinsGained);

        //ES3.Save(machineGUID + "-isUnlocked", isUnlocked);
        //ES3.Save(machineGUID + "-isPlaying", isPlaying);
        //ES3.Save(machineGUID + "-awayCheckPoint", awayCheckPoint);


    }

    public void LoadMachine()
    {
        machineCoins = ES3.Load(machineGUID + "-machineCoins", machineCoins);
        isUnlocked = ES3.Load(machineGUID + "-isUnlocked", isUnlocked);
        isPlaying = ES3.Load(machineGUID + "-isPlaying", isPlaying);
        coinsPerSecond = ES3.Load(machineGUID + "-coinsPerSecond", (double) coinsPerSecond);
        //accumulatedCoins = ES3.Load(machineGUID + "-accumulatedCoins", (double)accumulatedCoins);
        awayCheckPoint = ES3.Load(machineGUID + "-awayCheckPoint", DateTime.Now);
        totalCoinsGained = ES3.Load(machineGUID + "-totalCoinsGained", (double)totalCoinsGained);
    }
}
