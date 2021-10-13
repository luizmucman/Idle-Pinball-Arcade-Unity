using System;
using UnityEngine.Audio;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioMixerGroup sfxMixerGroup;
    public AudioMixerGroup bgmMixerGroup;
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

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        
        s.source.pitch = UnityEngine.Random.Range(s.pitchLow, s.pitchHigh);
        s.source.PlayOneShot(s.source.clip);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(bgmSounds, sound => sound.name == name);

        s.source.Play();
    }
}
