using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZavierZone : MonoBehaviour
{

    public List<Pizza> pizzas = new List<Pizza>();
    public List<Node> nodes = new List<Node>();

    public Action<Pizza> onPizzaAdd;
    public Action<Pizza> onPizzaRemove;
    public Action<Node> onNodeAdd;
    public Action<Node> onNodeRemove;


    public void OnTriggerEnter2D(Collider2D col)
    {
        print("enter");
        Pizza p = col.GetComponent<Pizza>();
        if (p != null)
        {
            pizzas.Add(p);
            onPizzaAdd(p);
            return;
        }

        Node n = col.GetComponent<Node>();

        if (n != null)
        {
            nodes.Add(n);
            onNodeAdd(n);
            return;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        print("exit");
        Pizza p = col.GetComponent<Pizza>();
        if (p != null)
        {
            if (pizzas.Remove(p))
                onPizzaRemove(p);
            return;
        }

        Node n = col.GetComponent<Node>();

        if (n != null)
        {
            if (nodes.Remove(n))
                onNodeRemove(n);
            return;
        }
    }
}
