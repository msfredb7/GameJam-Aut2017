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
    }
    public void PickedUpBy(Hero hero)
    {
        if (myNode == null)
            Debug.LogWarning("Weird, on viens de pick up une pizza qui netait pas sur une node.");
        myNode.pizza.Remove(this);

        myHero = hero;
        myHero.carriedPizza = this;

        if (pizzaPickedUp != null)
            pizzaPickedUp();
    }

    //public void MergePizza(Pizza other_Pizza)
    //{
    //	Quantity += other_Pizza.Quantity;
    //	Destroy(other_Pizza.gameObject);
    //	UpdateQuantityDisplay ();
    //}

    //public void splitPizza(int split_Quantity)
    //{
    //	Quantity -= split_Quantity;
    //	Pizza new_Pizza = Instantiate (gameObject, transform.position, transform.rotation).GetComponent<Pizza> ();
    //	new_Pizza.Quantity = split_Quantity;
    //	UpdateQuantityDisplay ();
    //}

    void UpdateQuantityDisplay()
    {
        qt.DisplayQuantity(Quantity);
    }
}
