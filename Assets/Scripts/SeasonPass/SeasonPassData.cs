using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeasonPassData
{
    public bool isPremium;
    public int seasonPassPoints;
    public int seasonPassLvl;
    public List<SeasonPassTier> seasonPassTiers;
    public List<int> seasonPassPointReqs;

    public void AddSeasonPoints(int points)
    {
        if(seasonPassLvl < seasonPassPointReqs.Count - 1)
        {
            for (int i = 0; i < points; i++)
            {
                seasonPassPoints++;
                if (seasonPassPoints >= seasonPassPointReqs[seasonPassLvl - 1])
                {
                    LevelUp();
                }
            }
            UIManager.instance.uiSeasonPassManager.SetExpSlider();
        }
    }

    public void ResetSeasonPass()
    {
        seasonPassPoints = 0;
        seasonPassLvl = 1;
    }

    private void LevelUp()
    {
        seasonPassLvl++;
        seasonPassPoints = 0;
        UIManager.instance.uiSeasonPassManager.SetLevelReached(seasonPassLvl);
    }

    public void SaveSeasonPassData()
    {
        ES3.Save("SeasonPass-isPremium", isPremium);
        ES3.Save("SeasonPass-Points", seasonPassPoints);
        ES3.Save("seasonPass-Level", seasonPassLvl);
        for(int i = 0; i < seasonPassTiers.Count; i++)
        {
            seasonPassTiers[i].SaveSeasonPassTier(i);
        }
    }

    public void LoadSeasonPassData()
    {
        isPremium = ES3.Load("SeasonPass-isPremium", false);
        seasonPassPoints = ES3.Load("SeasonPass-Points", 0);
        seasonPassLvl = ES3.Load("seasonPass-Level", 0);
        for (int i = 0; i < seasonPassTiers.Count; i++)
        {
            seasonPassTiers[i].LoadSeasonPassTiers(i);
        }
    }
}
