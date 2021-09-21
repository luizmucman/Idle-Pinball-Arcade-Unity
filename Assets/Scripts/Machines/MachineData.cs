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
    public ulong machineCost;
    public bool isUnlocked;
    public bool isPlaying;
    public bool isCurrentEvent;

    // Coin stats
    public ulong coinsPerSecond;
    public ulong accumulatedCoins;

    // UI Data
    public Sprite machineImage;

    // Away Data
    public DateTime awayCheckPoint;

    public void SaveMachine()
    {
        ES3.Save(machineName + "-isUnlocked", isUnlocked);
        ES3.Save(machineName + "-isPlaying", isPlaying);
        ES3.Save(machineName + "-coinsPerSecond", coinsPerSecond);
        ES3.Save(machineName + "-accumulatedCoins", accumulatedCoins);
        ES3.Save(machineName + "-awayCheckPoint", awayCheckPoint);
    }

    public void LoadMachine()
    {
        isUnlocked = ES3.Load(machineName + "-isUnlocked", false);
        isPlaying = ES3.Load(machineName + "-isPlaying", false);
        coinsPerSecond = ES3.Load(machineName + "-coinsPerSecond", (ulong) 0);
        accumulatedCoins = ES3.Load(machineName + "-accumulatedCoins", (ulong) 0);
        awayCheckPoint = ES3.Load(machineName + "-awayCheckPoint", DateTime.Now);
    }
}
