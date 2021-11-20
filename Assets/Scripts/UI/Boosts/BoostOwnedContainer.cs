using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostOwnedContainer : MonoBehaviour
{
    // UI Elements
    public Text boostLengthText;
    public Image boostImage;

    // Boost Data
    public BoostData boostData;

    public void SetBoostData(BoostData data)
    {
        boostData = data;

        TimeSpan duration = TimeSpan.FromHours(data.boostLength);

        if(duration.Days > 0)
        {
            boostLengthText.text = duration.ToString("dd' day(s)'");
        }
        else if(duration.Hours > 0)
        {
            boostLengthText.text = duration.ToString("hh'hr(s) 'mm'min(s)'");
        }
        else if(duration.Minutes > 0)
        {
            boostLengthText.text = duration.ToString("mm'min(s)'");
        }



        boostImage.sprite = PlayerManager.instance.boostDatabase.GetBoost(boostData.boostID).boostImg;
    }

    public void ClickBoost()
    {
        UIManager.instance.uiBoostsManager.descriptionWindow.gameObject.SetActive(true);
        UIManager.instance.uiBoostsManager.descriptionWindow.SetBoost(this);
    }
}
