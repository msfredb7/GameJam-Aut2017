using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardPrice : MonoBehaviour
{
    [SerializeField] private HeroCard herocard;
    [SerializeField] private Text priceText;
	// Use this for initialization
	void Start ()
	{
	    priceText.text = herocard.HeroDescription.price.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
