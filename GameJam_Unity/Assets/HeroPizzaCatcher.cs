using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPizzaCatcher : MonoBehaviour {
    public Hero myHero;


    void OnTriggerEnter2D(Collider2D col)
    {
        Pizza pizz = col.gameObject.GetComponent<Pizza>();
        if (pizz != null)
        {
            myHero.AttemptPizzaCatch(pizz);
        }
    }
}
