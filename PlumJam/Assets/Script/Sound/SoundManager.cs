using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    LightRain,
    HeavyRain,
    Jump,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    public List<Sound> sounds = new List<Sound>();

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType soundType)
    {
        audioSource.Stop();

        Sound sound = GetSound(soundType);
        audioSource.clip = sound.audioClip;
        audioSource.loop = true;
        audioSource.time = sound.skipTime;
        audioSource.Play();
    }

    public void PlaySoundOneShot(SoundType soundType)
    {
        //AudioSource audioSourceFor = new AudioSource();

        Sound sound = GetSound(soundType);
        audioSource.PlayOneShot(sound.audioClip);
    }

    //SoundType에 맞는 clip을 가져옴
    public Sound GetSound(SoundType soundType)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundType == soundType)
            {
                return s;
            }
        }
        //기본 값
        return sounds[0];
    }
}

[System.Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioClip audioClip;
    public float skipTime;
}