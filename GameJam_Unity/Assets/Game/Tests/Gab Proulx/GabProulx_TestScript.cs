using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabProulx_TestScript : MonoBehaviour {
	
	public Pizza pizza;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
		{
			//Pizza new_Pizza1 = Instantiate (pizza.gameObject, transform.position, transform.rotation).GetComponent<Pizza> ();
			//Pizza new_Pizza2 = Instantiate (pizza.gameObject, transform.position, transform.rotation).GetComponent<Pizza> ();
			//new_Pizza1.MergePizza (new_Pizza2);
			//new_Pizza1.splitPizza (1);
		}
	}
}