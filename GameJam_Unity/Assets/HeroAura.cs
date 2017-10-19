using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAura : MonoBehaviour
{
    private SpriteRenderer selectedHeroObject;

    // Use this for initialization
    void Start () {
		Game.OnGameStart += delegate
		{
            foreach (Transform child in transform)
            {
                if (child.name == "selected-hero")
                    selectedHeroObject = child.gameObject.GetComponent<SpriteRenderer>();
            }
            Game.HeroManager.onActiveHeroChanged += OnHeroSelectedChanged;
		};
	}

    void OnHeroSelectedChanged(Hero hero)
    {
        if (hero == GetComponent<Hero>())
        {
            //give aura
            selectedHeroObject.enabled = true;
        }
        else
        {
            //remove aura
            selectedHeroObject.enabled = false;
        }
    }
}
