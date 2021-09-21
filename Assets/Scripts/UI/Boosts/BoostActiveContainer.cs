using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostActiveContainer : MonoBehaviour
{
    // UI Elements
    public Text boostLengthText;
    public Image boostImage;

    // Boost Data
    public BoostData boostData;

    public void SetBoostData(BoostData data)
    {
        boostData = data;
        boostImage.sprite = boostData.boostImg;
        boostLengthText.text = "INACTIVE";
        boostImage.color = Color.gray;
    }

    public ulong UpdateBoostStatus()
    {
        TimeSpan duration = boostData.endTime - DateTime.Now;

        if (duration.Days > 0)
        {
            boostLengthText.text = duration.ToString("dd'day(s) 'hh'hr(s)'");
            boostImage.color = Color.white;
            return boostData.boostAmt;
        }
        else if (duration.Hours > 0)
        {
            boostLengthText.text = duration.ToString("hh'hr(s) 'mm'min(s)'");
            boostImage.color = Color.white;
            return boostData.boostAmt;
        }
        else if (duration.Minutes > 0)
        {
            boostLengthText.text = duration.ToString("mm'min(s) 'ss'sec(s)'");
            boostImage.color = Color.white;
            return boostData.boostAmt;
        }
        else if (duration.Seconds > 0)
        {
            boostLengthText.text = duration.ToString("ss'sec(s)'");
            boostImage.color = Color.white;
            return boostData.boostAmt;
        }
        else
        {
            boostLengthText.text = "INACTIVE";
            boostImage.color = Color.gray;
            boostData.inUse = false;
            return 0;
        }

    }
}
