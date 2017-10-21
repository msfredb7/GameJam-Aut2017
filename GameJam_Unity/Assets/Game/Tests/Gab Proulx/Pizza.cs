using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{

    public int Quantity = 1;
    public QuantityText qt;

    public Action pizzaPickedUp;
    public Action pizzaDropped;
    public Node myNode;
    public Hero myHero;

    void Start()
    {
        UpdateQuantityDisplay();
    }

    public void DroppedOn(Node node)
    {
        node.pizza.Add(this);
        if (myHero != null && myHero.carriedPizza == this)
            myHero.carriedPizza = null;
        if (pizzaDropped != null)
            pizzaDropped();
        myNode = node;
        myHero = null;
    }
    public void PickedUpBy(Hero hero)
    {
        if (myNode == null)
            Debug.LogWarning("Weird, on viens de pick up une pizza qui netait pas sur une node.");
        if (myNode.Order != null)
            return; // Si une pizza est sur une node qui a une Order, on doit pas la pickup !

        //transform.transform

        myNode.pizza.Remove(this);

        myHero = hero;
        myHero.carriedPizza = this;

        if (pizzaPickedUp != null)
            pizzaPickedUp();

        myNode = null;
    }

    public void Destroy()
    {
        if (myHero != null)
            myHero.carriedPizza = null;
        if (myNode != null)
            myNode.pizza.Remove(this);

        Destroy(gameObject);
    }

    void UpdateQuantityDisplay()
    {
        qt.DisplayQuantity(Quantity);
    }
}
