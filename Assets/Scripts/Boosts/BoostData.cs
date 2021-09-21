using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoostData
{
    public string boostID;
    public Sprite boostImg;
    public ulong boostAmt;
    public double boostLength;

    // In Use Stats
    public DateTime endTime;
    public bool inUse;

    // Inventory Stats
    public int qtyOwned;

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

    public ulong CheckBoost()
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
}
