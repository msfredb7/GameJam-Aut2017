using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

	public int Quantity = 1;
	public QuantityText qt;

    public Action pizzaPickedUp;

	void Start(){
		UpdateQuantityDisplay ();
	}

	public void MergePizza(Pizza other_Pizza)
	{
		Quantity += other_Pizza.Quantity;
		Destroy(other_Pizza.gameObject);
		UpdateQuantityDisplay ();
	}

	public void splitPizza(int split_Quantity)
	{
		Quantity -= split_Quantity;
		Pizza new_Pizza = Instantiate (gameObject, transform.position, transform.rotation).GetComponent<Pizza> ();
		new_Pizza.Quantity = split_Quantity;
		UpdateQuantityDisplay ();
	}

	void UpdateQuantityDisplay(){
		qt.DisplayQuantity (Quantity);
	}
}
