using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopHeroIcon : MonoBehaviour
{


    public Hero heroPrefab;
    public float cost;
    public HeroShop_Script parentPanel;

    private Hero heroInstance = null;
    private bool heroSelected = false;

    public void ReleaseDragHero()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Destroy(heroInstance);
            heroInstance = null;
            return;
        }
        AddHeroToWorld();
        Game.HeroManager.AddHero(heroInstance);
    }

    //  methode qui g�re le click de h�ro
    //  1er click, prend un h�ro et change le curseur
    //  2em click, lache le h�ro sur la map et remet le curseur
    public void ClickHero()
    {
        if (heroSelected == false)
        {
            heroSelected = true;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            heroInstance = Instantiate(heroPrefab, pos, Quaternion.identity);
            parentPanel.hideList();
        }
    }



    public void AddHeroToWorld()
    {
        heroSelected = false;
        heroInstance.SnapToNode();
        Game.HeroManager.AddHero(heroInstance);

        //TODO: Decrease Cash
    }
    public void DestroyHero()
    {
        heroSelected = false;
        if (heroInstance != null)
            Destroy(heroInstance.gameObject);
    }


    void Update()
    {
        if (heroSelected && heroInstance != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            heroInstance.transform.position = pos;

            if (Input.GetMouseButtonDown(1))
            {
                //Cancel !
                DestroyHero();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                //Paste
                //if (EventSystem.current.IsPointerOverGameObject())
                //{
                //    DestroyHero();
                //    return;
                //}
                AddHeroToWorld();
            }
        }
    }
}
