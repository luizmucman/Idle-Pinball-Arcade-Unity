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

    // Start is called before the first frame update
    void Start()
    {
        PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += OnPlayFabError;
    }

    private void OnPlayFabError(PlayFabError error)
    {
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
}
