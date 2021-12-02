using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Facebook.Unity;
using TapjoyUnity;

public class SDKInit : MonoBehaviour
{
    public void InitSDK()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
    .AddOauthScope("profile")
    .RequestServerAuthCode(false)
    .Build();
        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.Activate();

        FB.Init(OnFBInitComplete, OnFBHideUnity);
    }

    private void OnFBInitComplete()
    {
        Debug.Log("FB Init");
    }

    private void OnFBHideUnity(bool isUnityShown)
    {
        //do nothing.
    }
}
