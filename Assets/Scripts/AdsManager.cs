using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    string gameId = "4403846";
    string rewardedId = "Rewarded_iOS";
#elif UNITY_ANDROID
    string gameId = "4403847";
    string rewardedId = "Rewarded_Android";
#endif

    Action onRewardedAdSuccess;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }

    public void PlayRewardedAd(Action onSuccess)
    {
        onRewardedAdSuccess = onSuccess;

        if (Advertisement.IsReady(rewardedId))
        {
            Advertisement.Show(rewardedId);
        } 
        else
        {
            Debug.Log("Rewarded ad is not ready.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Unity Ads Ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Unity Ads Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Unity Ads Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedId && showResult == ShowResult.Finished)
        {
            onRewardedAdSuccess.Invoke();
        }
    }
}
