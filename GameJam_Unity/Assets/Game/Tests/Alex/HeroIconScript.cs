using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroIconScript : MonoBehaviour {

    public Button button;

    private Hero hero = null;

    void Start()
    {
        button.onClick.AddListener(delegate ()
        {
            if (hero != null)
                Game.HeroManager.SetActiveHero(hero);
        });
    }

	public void Display(Hero hero)
    {
        this.hero = hero;
    }
}
