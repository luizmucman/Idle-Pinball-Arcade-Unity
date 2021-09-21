using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    string gameId = "4241330";
    string rewardedId = "Rewarded_iOS";
#elif UNITY_ANDROID
    string gameId = "4241331";
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
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedId && showResult == ShowResult.Finished)
        {
            onRewardedAdSuccess.Invoke();
        }
    }
}
