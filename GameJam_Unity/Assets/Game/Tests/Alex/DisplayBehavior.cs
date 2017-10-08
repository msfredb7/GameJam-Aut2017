using CCC.Input;
using CCC.Manager;
using CCC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DisplayBehavior : MonoBehaviour {

    public const string SCENE_NAME = "CharacterOptions";

    public HeroBehaviorDisplay display;
    public Vector2 hiddenPos;
    public Vector2 semiHiddenPos;
    public Vector2 shownPos;
    public float transitionDuration;
    public MouseInputs selectNodeNotif;

    private bool quitting = false;

    public void Init (HeroBehavior behavior)
    {
        ShowPanel();
        display.Fill(behavior.hero);

        display.nodeRequest = (HeroBehaviorDisplay.NodeEvent callback) =>
        {
            selectNodeNotif.gameObject.SetActive(true);
            SemiHide();
            selectNodeNotif.screenClicked.AddListener((Vector2 clickPos) =>
            {
                callback(Game.Fastar.GetClosestNode(clickPos));
                ShowPanel();
                selectNodeNotif.screenClicked.RemoveAllListeners();
                selectNodeNotif.gameObject.SetActive(false);
            });
        };

        Game.HeroManager.onActiveHeroChanged += display.Fill;

    }

    public void Exit()
    {
        Game.HeroManager.onActiveHeroChanged -= display.Fill;
        display.ClearHB();
        quitting = true;

        HidePanel(() =>
        {
            Scenes.UnloadAsync(SCENE_NAME);
        });
    }



    public void ShowPanel(TweenCallback onComplete = null)
    {
        RectTransform rt = display.GetComponent<RectTransform>();
        rt.DOKill();
        Tween t = rt.DOAnchorPos(shownPos, transitionDuration).SetEase(Ease.OutSine);
        if (onComplete != null)
            t.OnComplete(onComplete);
    }

    public void SemiHide(TweenCallback onComplete = null)
    {
        RectTransform rt = display.GetComponent<RectTransform>();
        rt.DOKill();
        Tween t = rt.DOAnchorPos(semiHiddenPos, transitionDuration*0.5f).SetEase(Ease.InOutSine);
        if (onComplete != null)
            t.OnComplete(onComplete);
    }

    public void HidePanel(TweenCallback onComplete = null)
    {
        RectTransform rt = display.GetComponent<RectTransform>();
        rt.DOKill();
        Tween t = rt.DOAnchorPos(hiddenPos, transitionDuration).SetEase(Ease.InSine);
        if (onComplete != null)
            t.OnComplete(onComplete);
    }
}
