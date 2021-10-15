using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSettingsData
{
    public bool bgmMuted;
    public bool sfxMuted;

    public bool notificationsDisabled;


    public void Save()
    {
        ES3.Save("bgmMuted", bgmMuted);
        ES3.Save("sfxMuted", sfxMuted);
        ES3.Save("notificationsEnabled", notificationsDisabled);
    }

    public void Load()
    {
        bgmMuted = ES3.Load("bgmMuted", false);
        sfxMuted = ES3.Load("sfxMuted", false);
        notificationsDisabled = ES3.Load("notificationsEnabled", false);
    }
}
