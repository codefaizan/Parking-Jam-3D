using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] internal Sounds soundSO;
    GameObject soundGameObject;
    AudioSource audioSource;
    public static SoundManager i;
    public void PlaySound(Sounds.SoundType sound)
    {
        if (soundGameObject == null)
        {
            soundGameObject = new GameObject("Sound");
            audioSource = soundGameObject.AddComponent<AudioSource>();
        }

        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    AudioClip GetAudioClip(Sounds.SoundType sound)
    {

        foreach (Sounds.AudioClips audioClip in soundSO.clips)
        {
            if (audioClip.type == sound)
                return audioClip.clip;
        }
        print(sound + "sound, not found!");
        return null;
    }

    public void ToggleMute(bool on)
    {
        if (on)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
        
    }

    private void Awake()
    {
        i = this;
    }

}
