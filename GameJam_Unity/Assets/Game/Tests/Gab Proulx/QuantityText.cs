using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantityText : MonoBehaviour {

	public TextMesh text;

	public void DisplayQuantity(int val)
	{
        text.text = "";
        //text.text = "x" + val.ToString ();
    }

}
