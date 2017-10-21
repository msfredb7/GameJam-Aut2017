using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmouGrab : MonoBehaviour
{
    public Hero myHero;

    LinkedList<Node> nodes = new LinkedList<Node>();

    void OnTriggerEnter2D(Collider2D col)
    {
        Node node = col.gameObject.GetComponent<Node>();
        if (node == null)
            return;
        
        nodes.AddLast(node);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Node node = col.gameObject.GetComponent<Node>();
        if (node == null)
            return;
        
        nodes.Remove(node);
    }

    void FixedUpdate()
    {
        //Catch pizza
        if (myHero.carriedPizza == null)
            foreach (Node node in nodes)
            {
                Pizza pizz = node.GetPizza();
                if (pizz != null && myHero.AttemptPizzaCatch(pizz))
                    break;
            }

        //Check for orders
        if (myHero.carriedPizza != null)
            foreach (Node node in nodes)
            {
                if (node.Order != null)
                {
                    myHero.Drop(node);
                    break;
                }
            }
    }
}
