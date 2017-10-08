using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardPortrait : MonoBehaviour
{

    [SerializeField] private HeroCard heroCard;
    [SerializeField] private Image portrait;

	// Use this for initialization
	void Start ()
	{
	    portrait.sprite = heroCard.HeroDescription.heroFace;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
