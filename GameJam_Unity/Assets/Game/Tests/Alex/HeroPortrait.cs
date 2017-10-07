﻿using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HeroPortrait : MonoBehaviour {

    public Image heroPortrait;

    public GameObject heroIconPrefab;

    public GameObject teamOverlay;
    public GameObject teamContent;
    private float startPositionY;
    public float endPositionY;
    public float teamOverlayAnimDuration;

    private bool teamOverlayOpened;
    private bool clicked;

    void Start()
    {
        Game.OnGameReady += Init;
    }

    void Init()
    {
        teamOverlayOpened = false;
        clicked = false;
        startPositionY = teamOverlay.GetComponent<RectTransform>().anchoredPosition.y;
        Game.HeroManager.onHeroAdded += AddHeroIcon;
    }

	public void OnPortraitClicked()
    {
        Scenes.LoadAsync(DisplayBehavior.SCENE_NAME,UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void OnHeroTeamClicked()
    {
        if (clicked)
            return;
        clicked = true;
        if (teamOverlayOpened)
        {
            teamOverlay.GetComponent<RectTransform>().DOAnchorPosY(startPositionY, teamOverlayAnimDuration).OnComplete(delegate() {
                clicked = false;
            });
            teamOverlayOpened = false;
        }
        else
        {
            teamOverlay.GetComponent<RectTransform>().DOAnchorPosY(endPositionY, teamOverlayAnimDuration).OnComplete(delegate () {
                clicked = false;
            });
            teamOverlayOpened = true;
        }  
    }

    public void AddHeroIcon(Hero hero)
    {
        Instantiate(heroIconPrefab, teamContent.transform).GetComponent<HeroIconScript>().Display(hero);
    }

    public void OnNextClicked()
    {
        // TODO
    }

    public void OnCameraToggle()
    {
        // TODO
    }
}
