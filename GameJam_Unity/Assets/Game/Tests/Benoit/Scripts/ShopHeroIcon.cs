using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
