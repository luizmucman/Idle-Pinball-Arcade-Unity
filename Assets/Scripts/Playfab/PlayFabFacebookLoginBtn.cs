using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Facebook.Unity;
using System;

public class PlayFabFacebookLoginBtn : MonoBehaviour
{
    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;
    private Button btn;
    [SerializeField] private Text connectedTxt;

    private void Awake()
    {
        btn = GetComponent<Button>();
        if (PlayerManager.instance.facebookAccLinked)
        {
            ShowConnected();
        }
        else
        {
            ShowNotConnected();
        }

    }

    void Start()
    {
        PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += OnPlayFabError;
        PlayFabAuthService.OnFacebookLink += OnLinkSuccess;
    }

    private void OnLinkSuccess(LinkFacebookAccountResult success)
    {
        ShowConnected();
    }

    private void OnPlayFabError(PlayFabError error)
    {
        if (error.HttpCode == 400)
        {
            if (AccessToken.CurrentAccessToken != null)
            {
                _AuthService.AuthTicket = AccessToken.CurrentAccessToken.TokenString;
                _AuthService.LinkFacebook();
            }
        }


    }

    private void OnLoginSuccess(PlayFab.ClientModels.LoginResult success)
    {
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
        string deviceId = secure.CallStatic<string>("getString", contentResolver, "android_id");

        if (!deviceId.Equals(success.InfoResultPayload.AccountInfo.AndroidDeviceInfo.AndroidDeviceId))
        {
            PlayFabClientAPI.LinkAndroidDeviceID(new LinkAndroidDeviceIDRequest()
            {
                AndroidDeviceId = deviceId,
                OS = SystemInfo.operatingSystem,
                ForceLink = true,
                AndroidDevice = SystemInfo.deviceModel
            }, (result) =>
            {
                PlayerManager.instance.LoadFromPlayfab();
            }, (error) =>
            {
                Debug.Log(error);
            }); ;
        }
        else
        {

        }
    }

    public void OnClick()
    {
        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, OnHandleFBResult);
    }

    private void OnHandleFBResult(ILoginResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("FB Login Cancelled");
        }
        else if(result.Error != null) {
            Debug.Log(result.Error);
        }
        else
        {
            _AuthService.AuthTicket = result.AccessToken.TokenString;
            _AuthService.AuthenticateFacebook();
        }
    }

    private void ShowConnected()
    {
        btn.interactable = false;
        connectedTxt.text = "CONNECTED";
        connectedTxt.color = Color.green;
    }

    private void ShowNotConnected()
    {
        btn.interactable = true;
        connectedTxt.text = "NOT CONNECTED";
        connectedTxt.color = Color.red;
    }
}
