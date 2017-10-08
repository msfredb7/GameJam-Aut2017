using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopHeroIcon : MonoBehaviour {

    
    public Hero heroPrefab;
    public float cost;
    

    private Hero heroInstance = null;
    private bool heroSelected = false;

    public void clickDragHero()
    {
        heroSelected = true;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        heroInstance = Instantiate(heroPrefab, pos, Quaternion.identity);
    }

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

    //  methode qui gère le click de héro
    //  1er click, prend un héro et change le curseur
    //  2em click, lache le héro sur la map et remet le curseur
    public void ClickHero()
    {
        print("dans le click");

        if (heroSelected == false)
        {
            print("dans le false");
            heroSelected = true;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            heroInstance = Instantiate(heroPrefab, pos, Quaternion.identity);
        }

        print("sortie de click");
    }



    public void AddHeroToWorld()
    {
        heroSelected = false;
        heroInstance.SnapToNode();
    }

    // Update is called once per frame
    void Update()
    {
        if (heroSelected && heroInstance != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            heroInstance.transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                print("dans le true");
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Destroy(heroInstance);
                    heroInstance = null;
                    return;
                }
                AddHeroToWorld();
                Game.HeroManager.AddHero(heroInstance);
            }
        }
    }
}
