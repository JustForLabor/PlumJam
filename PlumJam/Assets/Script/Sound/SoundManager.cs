using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    LightRainBgm,

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

        AudioClip audioClip = GetSound(soundType);
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySoundOneShot(SoundType soundType)
    { 
        AudioClip audioClip = GetSound(soundType);
        audioSource.clip = audioClip;
        audioSource.PlayOneShot(audioClip);
    }

    //SoundType에 맞는 clip을 가져옴
    public AudioClip GetSound(SoundType soundType)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundType == soundType)
            {
                return s.audioClip;
            }
        }
        //기본 값
        return sounds[0].audioClip;
    }
}

[System.Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioClip audioClip;
}