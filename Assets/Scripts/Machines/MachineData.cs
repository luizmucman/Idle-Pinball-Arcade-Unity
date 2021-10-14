using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MachineData
{
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
    public double accumulatedCoins;

    // UI Data
    public Sprite machineImage;

    // Away Data
    public DateTime awayCheckPoint;

    public void SaveMachine()
    {
        ES3.Save(machineGUID + "-isUnlocked", isUnlocked);
        ES3.Save(machineGUID + "-isPlaying", isPlaying);
        ES3.Save(machineGUID + "-coinsPerSecond", coinsPerSecond);
        ES3.Save(machineGUID + "-accumulatedCoins", accumulatedCoins);
        ES3.Save(machineGUID + "-awayCheckPoint", awayCheckPoint);
        ES3.Save(machineGUID + "-totalCoinsGained", totalCoinsGained);
    }

    public void LoadMachine()
    {
        isUnlocked = ES3.Load(machineGUID + "-isUnlocked", isUnlocked);
        isPlaying = ES3.Load(machineGUID + "-isPlaying", false);
        coinsPerSecond = ES3.Load(machineGUID + "-coinsPerSecond", (double) 0);
        accumulatedCoins = ES3.Load(machineGUID + "-accumulatedCoins", (double) 0);
        awayCheckPoint = ES3.Load(machineGUID + "-awayCheckPoint", DateTime.Now);
        totalCoinsGained = ES3.Load(machineGUID + "-totalCoinsGained", 0);
    }
}
