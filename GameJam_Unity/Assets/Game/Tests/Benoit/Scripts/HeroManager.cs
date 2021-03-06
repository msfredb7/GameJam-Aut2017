using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroManager : MonoBehaviour
{
    public delegate void HeroEvent(Hero h);
    public List<Hero> listOwnedHero = new List<Hero>();
    private Hero activeHero = null;

    public event HeroEvent onHeroAdded;
    public event HeroEvent onActiveHeroChanged;

    public void AddHero(Hero newHero)
    {
        listOwnedHero.Add(newHero);
        activeHero = newHero;
        newHero.onClick += SetActiveHero;
        onActiveHeroChanged.Invoke(newHero);

        SpriteRenderer spr = newHero.faceSpriteRenderer;
        spr.sprite = newHero.heroDescription.heroFace;

        if (onHeroAdded != null)
            onHeroAdded(newHero);
    }

    public Hero getActiveHero()
    {
        return activeHero;
    }

    public void SetActiveHero(Hero hero)
    {
        if (hero == null)
            return;
        activeHero = hero;
        if (onActiveHeroChanged != null)
            onActiveHeroChanged.Invoke(activeHero);
    }

    public List<Hero> GetActiveHeroList()
    {
        return listOwnedHero;
    }

    public int FindHeroIndex(Hero hero)
    {
        for (int i = 0; i < listOwnedHero.Count; i++)
        {
            if (listOwnedHero[i] == hero)
                return i;
        }
        return -1;
    }

    public Hero FindNextHero(Hero hero)
    {
        for (int i = 0; i < listOwnedHero.Count; i++)
        {
            if (listOwnedHero[i] == hero)
            {
                if ((i + 1) >= listOwnedHero.Count)
                    return listOwnedHero[0];
                else
                    return listOwnedHero[i + 1];
            }
        }
        return null;
    }

    public Hero FindNextHero()
    {
        for (int i = 0; i < listOwnedHero.Count; i++)
        {
            if (listOwnedHero[i] == activeHero)
            {
                if ((i + 1) >= listOwnedHero.Count)
                    return listOwnedHero[0];
                else
                    return listOwnedHero[i + 1];
            }
        }
        return null;
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
            if (node != null)
            {
                GoAction newAction = new GoAction();
                newAction.GetHeroActionInfo().GiveNode(node);
                activeHero.behavior.AddTemporaryAction(newAction);
            }
        }
    }

}
