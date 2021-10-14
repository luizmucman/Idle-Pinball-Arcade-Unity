using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoostInUseSave {
    public string boostID;

    public string endTime;
    public bool inUse;

    public BoostInUseSave(BoostData data)
    {
        boostID = data.boostID;

        endTime = data.endTime.ToString();
        inUse = data.inUse;
    }
}

[Serializable]
public class BoostOwnedSave
{
    public string boostID;

    public int qtyOwned;
    public double boostLength;

    public BoostOwnedSave(BoostData data)
    {
        boostID = data.boostID;
        qtyOwned = data.qtyOwned;
        boostLength = data.boostLength;
    }
}

[System.Serializable]
public class BoostData
{
    public string boostID;
    public Sprite boostImg;
    public double boostAmt;


    // In Use Stats
    public DateTime endTime;
    public bool inUse;

    // Inventory Stats
    public int qtyOwned;
    public double boostLength;

    // Functions For Active
    public void AddTime(double hours)
    {
        if(!inUse)
        {
            endTime = DateTime.Now;
            inUse = true;
        }

        endTime = endTime.AddHours(hours);

    }

    public double CheckBoost()
    {
        if(inUse)
        {
            TimeSpan timeDiff = endTime - DateTime.Now;
            if(timeDiff.Ticks <= 0)
            {
                inUse = false;
            }
            return boostAmt;
        }
        else
        {
            return 0;
        }
    }

    public string FormatDuration()
    {
        TimeSpan duration = TimeSpan.FromHours(boostLength);
        string boostLengthText;

        if (duration.Days > 0)
        {
            boostLengthText = duration.ToString("dd' day(s)'");
        }
        else if (duration.Hours > 0)
        {
            boostLengthText = duration.ToString("hh' hr(s)'");
            if (duration.Minutes > 0)
            {
                boostLengthText += duration.ToString(" mm' min(s)'");
            }
        }
        else
        {
            boostLengthText = duration.ToString("mm' min(s)'");
        }
        return boostLengthText;
    }

    public void SaveBoostDatabase()
    {
        ES3.Save(boostID + "-database-endTime", endTime);
        ES3.Save(boostID + "-database-inUse", inUse);
    }

    public void LoadBoostDatabase()
    {
        if(ES3.KeyExists(boostID + "-database-endTime"))
        {
            endTime = ES3.Load(boostID + "-database-endTime", endTime);
            inUse = ES3.Load(boostID + "-database-inUse", inUse);
        }
    }

}
