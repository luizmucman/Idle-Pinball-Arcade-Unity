using System;
using UnityEngine.Audio;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    // Audio Sources
    public AudioMixerGroup sfxMixerGroup;
    public AudioMixerGroup bgmMixerGroup;

    // Sounds
    public Sound[] sfxSounds;
    public Sound[] bgmSounds;

    private void Awake()
    {
        foreach (Sound s in sfxSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.outputAudioMixerGroup = sfxMixerGroup;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

        }

        foreach (Sound s in bgmSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.loop = true;

            s.source.outputAudioMixerGroup = bgmMixerGroup;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    private void Start()
    {

        if (ES3.Load("bgmMuted", false))
        {
            bgmMixerGroup.audioMixer.SetFloat("BGMVolume", -80f);
        }
        else
        {
            bgmMixerGroup.audioMixer.SetFloat("BGMVolume", 0f);
        }

        if (ES3.Load("sfxMuted", false))
        {
            sfxMixerGroup.audioMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            sfxMixerGroup.audioMixer.SetFloat("SFXVolume", 0f);
        }

        PlayerManager.instance.soundsManager.PlayMusic("bgmMain");
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        
        s.source.pitch = UnityEngine.Random.Range(s.pitchLow, s.pitchHigh);
        s.source.PlayOneShot(s.source.clip);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(bgmSounds, sound => sound.name == name);

        if (!s.source.isPlaying)
        {
            s.source.Play();
        }

    }

    public void MuteMusic()
    {
        PlayerManager.instance.playerSettingsData.bgmMuted = true;
        bgmMixerGroup.audioMixer.SetFloat("BGMVolume", -80f);
    }

    public void UnmuteMusic()
    {
        PlayerManager.instance.playerSettingsData.bgmMuted = false;
        bgmMixerGroup.audioMixer.SetFloat("BGMVolume", 0f);
    }

    public void MuteSfx()
    {
        PlayerManager.instance.playerSettingsData.sfxMuted = true;

        bgmMixerGroup.audioMixer.SetFloat("SFXVolume", -80f);
    }

    public void UnmuteSfx()
    {
        PlayerManager.instance.playerSettingsData.sfxMuted = false;

        bgmMixerGroup.audioMixer.SetFloat("SFXVolume", 0f);
    }
}
