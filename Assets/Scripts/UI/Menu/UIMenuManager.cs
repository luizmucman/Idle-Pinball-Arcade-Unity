using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public UIWindow settingsWindow;

    private SoundsManager soundsManager;

    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;

    // Audio Sources
    public AudioMixerGroup sfxSource;
    public AudioMixerGroup bgmSource;

    // Audio UI Buttons
    public Button sfxButton;
    public Button bgmButton;

    // Audio Button Sprites
    public Sprite audioButtonUnmuted;
    public Sprite audioButtonMuted;

    // Bottom Text
    [SerializeField] private Text playerId;
    [SerializeField] private Text versionNum;

    private void Start()
    {
        soundsManager = PlayerManager.instance.soundsManager;

        if (PlayerManager.instance.playerSettingsData.bgmMuted)
        {
            bgmButton.image.sprite = audioButtonMuted;

        }
        else
        {
            bgmButton.image.sprite = audioButtonUnmuted;

        }

        if (PlayerManager.instance.playerSettingsData.sfxMuted)
        {
            sfxButton.image.sprite = audioButtonMuted;
        }
        else
        {
            sfxButton.image.sprite = audioButtonUnmuted;
        }



        playerId.text = "Player Id: " + PlayerManager.instance.playFabID;
        versionNum.text = "v" + Application.version;
    }

    public void ToggleSfx()
    {
        if (PlayerManager.instance.playerSettingsData.sfxMuted)
        {

            soundsManager.UnmuteSfx();
            sfxButton.image.sprite = audioButtonUnmuted;
            
        }
        else
        {
            sfxButton.image.sprite = audioButtonMuted;
            soundsManager.MuteSfx();
        }

        PlayerManager.instance.playerSettingsData.Save();
    }

    public void ToggleBgm()
    {
        if (PlayerManager.instance.playerSettingsData.bgmMuted)
        {
            bgmButton.image.sprite = audioButtonUnmuted;
            soundsManager.UnmuteMusic();
        }
        else
        {
            bgmButton.image.sprite = audioButtonMuted;
            soundsManager.MuteMusic();
        }

        PlayerManager.instance.playerSettingsData.Save();
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.OpenAnim();
        UIManager.instance.ShowOverlay();
    }

    public void CloseSettingsWindow()
    {
        if(settingsWindow.isActiveAndEnabled)
        {
            settingsWindow.CloseAnim();
            UIManager.instance.HideOverlay();
        }

    }

    public void SetPlayerId(string id)
    {
        playerId.text = "PlayerID: " + id;
    }
}
