using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public GameObject settingsWindow;

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
        if (PlayerManager.instance.playerSettingsData.bgmMuted)
        {
            bgmSource.audioMixer.SetFloat("BGMVolume", -80f);
            bgmButton.image.sprite = audioButtonMuted;
        }
        else 
        {
            bgmSource.audioMixer.SetFloat("BGMVolume", 0f);
            bgmButton.image.sprite = audioButtonUnmuted;
        }

        if (PlayerManager.instance.playerSettingsData.sfxMuted)
        {
            sfxSource.audioMixer.SetFloat("SFXVolume", -80f);
            sfxButton.image.sprite = audioButtonMuted;
        }
        else
        {
            sfxSource.audioMixer.SetFloat("SFXVolume", 0f);
            sfxButton.image.sprite = audioButtonUnmuted;
        }

        playerId.text = "Player Id: " + PlayerManager.instance.playFabID;
        versionNum.text = "v" + Application.version;
    }

    public void ToggleSfx()
    {
        if (PlayerManager.instance.playerSettingsData.sfxMuted)
        {
            sfxSource.audioMixer.SetFloat("SFXVolume", 0f);
            sfxButton.image.sprite = audioButtonUnmuted;
            PlayerManager.instance.playerSettingsData.sfxMuted = false;
        }
        else
        {
            sfxSource.audioMixer.SetFloat("SFXVolume", -80f);
            sfxButton.image.sprite = audioButtonMuted;
            PlayerManager.instance.playerSettingsData.sfxMuted = true;
        }
    }

    public void ToggleBgm()
    {
        if (PlayerManager.instance.playerSettingsData.bgmMuted)
        {
            bgmSource.audioMixer.SetFloat("BGMVolume", 0f);
            bgmButton.image.sprite = audioButtonUnmuted;
            PlayerManager.instance.playerSettingsData.bgmMuted = false;
        }
        else
        {
            bgmSource.audioMixer.SetFloat("BGMVolume", -80f);
            bgmButton.image.sprite = audioButtonMuted;
            PlayerManager.instance.playerSettingsData.bgmMuted = true;
        }
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
        UIManager.instance.ShowOverlay();
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
        UIManager.instance.HideOverlay();
    }

    public void SetPlayerId(string id)
    {
        playerId.text = "PlayerID: " + id;
    }
}
