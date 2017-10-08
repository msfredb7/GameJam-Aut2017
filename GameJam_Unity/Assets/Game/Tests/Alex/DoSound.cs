using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSound : MonoBehaviour {

    public enum SoundType { music = 0, sfx = 1, voice = 2 }
    public SoundType currentSoundType;

    public AudioClip sound;

    public bool loopOnPlay = false;
    public float volume = 1;
    public bool doOnStart = false;
    public bool doOnActivation = false;
    public bool addDelay = false;
    public float delay = 1;

    private bool soundPlayed = false;

	void Start ()
    {
        Game.OnGameStart += Init;
        soundPlayed = false;
    }

    void Init()
    {
        if (doOnStart)
            Play();
    }
	
	void Update ()
    {
        if (doOnActivation)
        {
            if (gameObject.activeSelf && !soundPlayed)
            {
                soundPlayed = true;
            }
        }
	}

    void Play()
    {
        switch (currentSoundType)
        {
            case SoundType.music:
                if (addDelay)
                {
                    DelayManager.LocalCallTo(delegate ()
                    {
                        SoundManager.PlayMusic(sound, loopOnPlay, volume);
                    }, delay, this);
                } else
                    SoundManager.PlayMusic(sound, loopOnPlay, volume);
                break;
            case SoundType.sfx:
                if (addDelay)
                    SoundManager.PlaySFX(sound, delay, volume);
                else
                    SoundManager.PlaySFX(sound, 0, volume);
                break;
            case SoundType.voice:
                if (addDelay)
                    SoundManager.PlayVoice(sound, delay, volume);
                else
                    SoundManager.PlayVoice(sound, 0, volume);
                break;
            default:
                break;
        }
    }
}
