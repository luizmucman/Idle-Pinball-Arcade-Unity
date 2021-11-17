using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    Action onRewardedAdSuccess;

    // Start is called before the first frame update
    void Start()
    {
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSource.Agent.validateIntegration();
    }

    public void PlayRewardedAd(Action onSuccess, string placement)
    {
        onRewardedAdSuccess = onSuccess;

        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo(placement);
        } 
        else
        {
            Debug.Log("Rewarded ad is not ready.");
        }
    }

    public void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
        //TODO - here you can reward the user according to the reward name and amount
        onRewardedAdSuccess.Invoke();
    }
}
