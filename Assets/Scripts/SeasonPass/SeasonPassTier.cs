using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeasonPassTier
{
    public bool containsFreeReward;

    public SeasonPassRewards premiumReward;
    public SeasonPassRewards freeReward;

    public void SaveSeasonPassTier(int i)
    {
        ES3.Save("SeasonPassTierPremium-" + i, premiumReward.isClaimed);
        ES3.Save("SeasonPassTierFree-" + i, freeReward.isClaimed);

    }

    public void LoadSeasonPassTiers(int i)
    {
        premiumReward.isClaimed = ES3.Load("SeasonPassTierPremium-" + i, premiumReward.isClaimed);
        freeReward.isClaimed = ES3.Load("SeasonPassTierFree-" + i, freeReward.isClaimed);
    }
}
