using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroIconScript : MonoBehaviour {

    public Button button;
    public Color selectedColor;
    public Color baseColor;
    public Image background;
    public Image icon;
    public Text heroName;

    private Hero hero = null;

    void Start()
    {
        Game.HeroManager.onActiveHeroChanged += CheckEmphase;
        button.onClick.AddListener(delegate ()
        {
            if (hero != null)
                Game.HeroManager.SetActiveHero(hero);
        });
    }

    public void CheckEmphase(Hero hero)
    {
        if(hero == this.hero)
        {
            background.color = selectedColor;
        } else
        {
            background.color = baseColor;
        }
    }

	public void Display(Hero hero)
    {
        this.hero = hero;
        icon.sprite = hero.heroDescription.heroFace;
        heroName.text = hero.heroDescription.name;
    }
}
