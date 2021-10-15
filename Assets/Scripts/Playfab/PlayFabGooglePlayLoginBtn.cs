using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using PlayFab.ClientModels;
using PlayFab;


public class PlayFabGooglePlayLoginBtn : MonoBehaviour
{

    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;
    private Button btn;
    [SerializeField] private Text connectedTxt;

    private void Awake()
    {
        btn = GetComponent<Button>();
        if (PlayerManager.instance.googleAccLinked)
        {
            ShowConnected();
        }
        else
        {
            ShowNotConnected();
        }

    }

    private void OnPlayFabError(PlayFabError error)
    {
        UIManager.instance.uiErrorWindow.SetErrorDesc(error.HttpCode + ": " + error.ErrorMessage);
        Social.localUser.Authenticate((success) =>
        {
            if (success)
            {
                var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                _AuthService.AuthTicket = serverAuthCode;
                _AuthService.LinkGooglePlayGames();
            }
        });

    }

    private void OnLoginSuccess(LoginResult success)
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

        Social.localUser.Authenticate((success) =>
        {
            if (success)
            {
                var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                _AuthService.AuthTicket = serverAuthCode;
                _AuthService.Authenticate(Authtypes.Google);
            }
        });
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

    private void OnGoogleLink(LinkGoogleAccountResult success)
    {
        ShowConnected();
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += OnPlayFabError;
        PlayFabAuthService.OnGoogleLink += OnGoogleLink;
    }

    private void UnsubscribeEvents()
    {
        PlayFabAuthService.OnLoginSuccess -= OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError -= OnPlayFabError;
        PlayFabAuthService.OnGoogleLink -= OnGoogleLink;
    }
}
