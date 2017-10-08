using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CCC.Manager;

public class DoSoundOnClick : MonoBehaviour {

    public enum SoundType { music = 0, sfx = 1, voice = 2 }
    public SoundType currentSoundType;
    public AudioClip sound;

    void Start ()
    {
		if(GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.AddListener(delegate ()
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
            });
        }
	}
}
