using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostForAdButton : MonoBehaviour
{
    private AdsManager adsManager;

    private void Start()
    {
        adsManager = PlayerManager.instance.GetComponent<AdsManager>();
    }

    public void WatchAd()
    {
        if (PlayerManager.instance.isAdFree)
        {
            RewardAdBoost();
        }
        else
        {
            adsManager.PlayRewardedAd(RewardAdBoost);
        }
    }

    private void RewardAdBoost()
    {
        BoostData boost = new BoostData();

        boost.boostID = "BOOAD";
        boost.boostLength = 2;

        PlayerManager.instance.UseBoost(boost);
    }
}
