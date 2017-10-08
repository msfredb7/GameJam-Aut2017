using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Manager;

public class DoClickSound : MonoBehaviour {

    public enum SoundType { music = 0, sfx = 1, voice = 2 }
    public SoundType currentSoundType;
    public AudioClip sound;
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (currentSoundType)
            {
                case SoundType.music:
                    SoundManager.PlayMusic(sound);
                    break;
                case SoundType.sfx:
                    SoundManager.PlaySFX(sound);
                    break;
                case SoundType.voice:
                    SoundManager.PlayVoice(sound);
                    break;
                default:
                    break;
            }
        }
	}
}
