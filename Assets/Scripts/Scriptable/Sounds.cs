using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MySO/Sounds")]
public class Sounds : ScriptableObject
{
    public AudioClips[] clips;
    public enum SoundType
    {
        carMove,
        carOut,
        passLevel,
        fireworks
    }

    [System.Serializable]
    public class AudioClips
    {
        public SoundType type;
        public AudioClip clip;
    }

}
