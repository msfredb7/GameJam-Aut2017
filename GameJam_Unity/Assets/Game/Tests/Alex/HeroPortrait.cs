using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroPortrait : MonoBehaviour {

    public Image heroPortrait;
    public Image brainImage;
    public Color openBrainColor;

    public GameObject heroIconPrefab;

    public GameObject teamOverlay;
    public GameObject teamContent;
    private float startPositionY;
    public float endPositionY;
    public float teamOverlayAnimDuration;
	public Toggle toggleRef;

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
        Game.HeroManager.onActiveHeroChanged += SetCurrentHero;
        Game.HeroManager.onHeroAdded += AddHeroIcon;
    }

	public void OnPortraitClicked()
    {
        if (Scenes.Exists(DisplayBehavior.SCENE_NAME))
        {
            brainImage.color = Color.white;
            Scenes.GetActive(DisplayBehavior.SCENE_NAME).FindRootObject<DisplayBehavior>().Exit();
        }
        else
        {
            brainImage.color = openBrainColor;
            if (Game.HeroManager.getActiveHero() != null)
                Scenes.LoadAsync(DisplayBehavior.SCENE_NAME, LoadSceneMode.Additive, OnDisplayBehaviorLoaded);
        }
    }

    public void OnDisplayBehaviorLoaded(Scene scene)
    {
        scene.FindRootObject<DisplayBehavior>().Init(Game.HeroManager.getActiveHero().behavior);
    }

    public void OnHeroTeamClicked()
    {
        if (clicked)
            return;
        clicked = true;
        if (teamOverlayOpened)
        {
            teamOverlay.GetComponent<RectTransform>().DOAnchorPosY(startPositionY, teamOverlayAnimDuration).SetEase(Ease.InSine).OnComplete(delegate() {
                clicked = false;
            });
            teamOverlayOpened = false;
        }
        else
        {
            teamOverlay.GetComponent<RectTransform>().DOAnchorPosY(endPositionY, teamOverlayAnimDuration).SetEase(Ease.OutSine).OnComplete(delegate () {
                clicked = false;
            });
            teamOverlayOpened = true;
        }  
    }

    public void AddHeroIcon(Hero hero)
    {
        GameObject newHeroIcon;
        newHeroIcon = Instantiate(heroIconPrefab, teamContent.transform);
        newHeroIcon.GetComponent<HeroIconScript>().Display(hero);
        UpdateAllHeroIcon();
    }

    public void OnNextClicked()
    {
        if(Game.HeroManager.FindNextHero(Game.HeroManager.getActiveHero()) != Game.HeroManager.getActiveHero())
            Game.HeroManager.SetActiveHero(Game.HeroManager.FindNextHero(Game.HeroManager.getActiveHero()));
    }

    public void OnCameraToggle()
    {
        // TODO
    }

    public void UpdateAllHeroIcon()
    {
        foreach (Transform child in teamContent.transform)
        {
            child.gameObject.GetComponent<HeroIconScript>().CheckEmphase(Game.HeroManager.getActiveHero());
        }
    }

    public void SetCurrentHero(Hero hero)
    {
        heroPortrait.sprite = hero.heroDescription.heroBody;
    }
}
