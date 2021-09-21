using System;
using UnityEngine.Audio;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioMixerGroup sfxMixerGroup;
    public Sound[] sfxSounds;

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
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        
        s.source.pitch = UnityEngine.Random.Range(s.pitchLow, s.pitchHigh);
        s.source.PlayOneShot(s.source.clip);
    }
}
