using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Manager;
using UnityEngine.UI;
using DG.Tweening;

public class FollowHero : MonoBehaviour {

    public AudioClip sfx_zoom;
    public AudioSource sf_Source;

	private Toggle toggleHero;
	private bool isNotifQueued;
    private bool gameReady = false;

    public Image x;

	void Start () 
	{
        Game.OnGameReady += Init;
    }

    void Init()
    {
        x.color = x.color.ChangedAlpha(0);
        gameReady = true;
        if (GetComponent<Toggle>() != null)
            toggleHero = GetComponent<Toggle>();
    }
	
	void Update () 
	{
        if (gameReady)
        {
            if (toggleHero.isOn)
            {
                x.DOFade(0, 0.5f);
                Follow();
            } else
            {
                x.DOFade(1, 0.5f);
            }
        }
	}

	void Follow()
	{
        Hero hero = Game.HeroManager.getActiveHero ();
		if (hero != null) {
			Camera.main.transform.position = new Vector3 (hero.transform.position.x, hero.transform.position.y, Camera.main.transform.position.z);
		}
		else
		{
			if (!isNotifQueued)
			{
				isNotifQueued = true;
				NotificationQueue.PushNotification ("Il faut au moins un héro pour le suivre.");
				DelayManager.LocalCallTo (delegate {
					isNotifQueued = false;
				}, 5f, this);
			}
		}
	}


    public void toggleChange()
    {
        //SoundManager.PlayStaticSFX(sfx_zoom);
        if (toggleHero.isOn)
        {
            sf_Source.pitch = 1;
            sf_Source.time = 0;
            sf_Source.Play();
        }
        else
        {
            sf_Source.pitch = -1;
            sf_Source.time = (sfx_zoom.length) - 0.01f;
            sf_Source.Play();
            sf_Source.pitch = 1;
            sf_Source.time = 0;
            //SoundManager.PlaySFX(sfx_zoom, 0, 1, sf_Source);
        }
            

    }
}