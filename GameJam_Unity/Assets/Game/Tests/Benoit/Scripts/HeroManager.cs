using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroManager : MonoBehaviour
{

    private List<Hero> listOwnedHero = new List<Hero>();
    private Hero activeHero = null;

    public Action<Hero> onHeroAdded;

    public void AddHero(Hero newHero)
    {
        listOwnedHero.Add(newHero);
        activeHero = newHero;
        newHero.onClick += SetActiveHero;

        if (onHeroAdded != null)
            onHeroAdded(newHero);
    }

    public Hero getActiveHero()
    {
        return activeHero;
    }

    public void SetActiveHero(Hero hero)
    {
        activeHero = hero;
        if (activeHero != null)
            print("hero active");
    }


    private void Update()
    {
        if (activeHero != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                MoveHeroTo();
            }
        }
    }

    private void MoveHeroTo()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        else
        {

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Node node = Game.Fastar.GetClosestNode(pos);
            if(node != null)
                activeHero.brain.GoToNode(node);
        }
    }

}
