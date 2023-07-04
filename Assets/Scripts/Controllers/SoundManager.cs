using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void ToggleMute(bool on)
    {
        if (on)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }

}
