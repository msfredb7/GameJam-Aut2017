using System;
using System.Collections;
using System.Collections.Generic;
using CCC.Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackDisplay : MonoBehaviour
{

    public Text FeedBackText;
    public GameObject CashPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayFeedbackAnimation(int income)
    {
        FeedBackText.enabled = true;
        FeedBackText.GetComponent<Outline>().enabled = true;
        FeedBackText.GetComponent<Outline>().DOFade(1, 0);
        DelayManager.LocalCallTo(delegate
        {
            FeedBackText.enabled = false;
            FeedBackText.GetComponent<Outline>().enabled = false;
        }, 3,this);

        if (income < 0)
        {
            //negatif
            FeedBackText.text = income + "$";
            FeedBackText.DOColor(new Color(0.59f, 0.25f, 0.25f), 0);
        }
        else
        {
            //positif
            FeedBackText.text = "+" + income + "$";
            FeedBackText.color = new Color(0.42f, 0.59f, 0.26f);
        }
        
        FeedBackText.transform.position = CashPanel.transform.position + new Vector3(0,-50, 0);
        FeedBackText.transform.DOMoveY(-20, 1).SetRelative();

        FeedBackText.DOFade(0, 3);
        FeedBackText.GetComponent<Outline>().DOFade(0, 3);
    }
}
