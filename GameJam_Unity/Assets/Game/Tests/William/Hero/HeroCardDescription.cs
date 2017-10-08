using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardDescription : MonoBehaviour
{
    public HeroCard heroCard;

    [SerializeField] private Text heroNameText;
    [SerializeField] private Text heroDescriptionText;

    void Awake()
    {
        enabled = false;
        Game.OnGameStart += delegate
        {
            enabled = true;
        };
    }

    void Start()
    {
        
        heroNameText.text = heroCard.HeroDescription.name;
        heroDescriptionText.text = heroCard.HeroDescription.heroDescription;
    }
}
