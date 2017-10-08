using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Manager;
using UnityEngine.UI;

public class FollowHero : MonoBehaviour {

	private Toggle toggleHero;
	private bool isNotifQueued;

	// Use this for initialization
	void Start () 
	{
		toggleHero = GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (toggleHero.isOn)
		{
			Follow ();
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