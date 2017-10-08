using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAura : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedHeroObject;

	// Use this for initialization
	void Start () {
		Game.OnGameStart += delegate
		{
		    Game.HeroManager.onActiveHeroChanged += OnHeroSelectedChanged;
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnHeroSelectedChanged(Hero hero)
    {
        SpriteRenderer renderer = selectedHeroObject.GetComponent<SpriteRenderer>();
        if (hero == GetComponent<Hero>())
        {
            //give aura
            renderer.enabled = true;
        }
        else
        {
            //remove aura
            renderer.enabled = false;
        }
    }
}
