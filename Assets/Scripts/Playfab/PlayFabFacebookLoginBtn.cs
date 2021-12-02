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
        UnsubscribeEvents();
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


    private void OnLinkSuccess(LinkFacebookAccountResult success)
    {
        ShowConnected();
        UnsubscribeEvents();
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
            else
            {
                UnsubscribeEvents();
            }
        }
        else
        {
            UIManager.instance.uiErrorWindow.SetErrorDesc(error.HttpCode + ": " + error.ErrorMessage);
            UnsubscribeEvents();
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
                UnsubscribeEvents();
            }, (error) =>
            {
                Debug.Log(error);
                UIManager.instance.uiErrorWindow.SetErrorDesc(error.ErrorMessage);
            }); ;
        }
        else
        {

        }
    }

    public void OnClick()
    {
        SubscribeEvents();

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

    private void SubscribeEvents()
    {
        PlayFabAuthService.OnFacebookLoginSuccess += OnLoginSuccess;
        PlayFabAuthService.OnPlayFabFacebookLoginError += OnPlayFabError;
        PlayFabAuthService.OnFacebookLink += OnLinkSuccess;
    }

    private void UnsubscribeEvents()
    {
        PlayFabAuthService.OnFacebookLoginSuccess -= OnLoginSuccess;
        PlayFabAuthService.OnPlayFabFacebookLoginError -= OnPlayFabError;
        PlayFabAuthService.OnFacebookLink -= OnLinkSuccess;
    }
}
