using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeasonPassData
{
    public bool isPremium;
    public int seasonPassLvl;
    public List<SeasonPassTier> seasonPassTiers;
    public List<double> seasonPassPointReqs;

    public void ResetSeasonPass()
    {
        seasonPassLvl = 1;
    }

    public void CheckSeasonPassProgress()
    {
        double totalEventCoinsGained = PlayerManager.instance.playerMachineData.currentEventMachineData.totalCoinsGained;
        double totalCoinsAdjusted = 0;
        double currentCoinsNeeded = 0;
        
        if(seasonPassLvl > 1)
        {
            totalCoinsAdjusted = totalEventCoinsGained - seasonPassPointReqs[seasonPassLvl - 2];
            currentCoinsNeeded = seasonPassPointReqs[seasonPassLvl - 1] - seasonPassPointReqs[seasonPassLvl - 2];
        }
        else
        {
            totalCoinsAdjusted = totalEventCoinsGained;
            currentCoinsNeeded = seasonPassPointReqs[seasonPassLvl - 1];
        }

        if(totalCoinsAdjusted >= currentCoinsNeeded)
        {
            LevelUp();
        }

        UIManager.instance.uiSeasonPassManager.SetExpSlider(currentCoinsNeeded, totalCoinsAdjusted);
    }

    private void LevelUp()
    {
        seasonPassLvl++;
        UIManager.instance.uiSeasonPassManager.SetLevelReached(seasonPassLvl);
        ES3.Save("seasonPass-Level", seasonPassLvl);
    }

    public void SaveSeasonPassData()
    {
        //ES3.Save("SeasonPass-isPremium", isPremium);
        //ES3.Save("seasonPass-Level", seasonPassLvl);

        ES3.Save("season-pass-tiers", seasonPassTiers);

        //for(int i = 0; i < seasonPassTiers.Count; i++)
        //{
        //    seasonPassTiers[i].SaveSeasonPassTier(i);
        //}
    }

    public void LoadSeasonPassData()
    {
        isPremium = ES3.Load("SeasonPass-isPremium", isPremium);
        seasonPassLvl = ES3.Load("seasonPass-Level", (int)seasonPassLvl);

        seasonPassTiers = ES3.Load("season-pass-tiers", seasonPassTiers);

        //for (int i = 0; i < seasonPassTiers.Count; i++)
        //{
        //    seasonPassTiers[i].LoadSeasonPassTiers(i);
        //}
    }
}
