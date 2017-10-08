using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Manager;
using UnityEngine.UI;
using DG.Tweening;

public class FollowHero : MonoBehaviour {

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
}