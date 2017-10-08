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
    }



    public void ShowPanel()
    {
        RectTransform rt = display.GetComponent<RectTransform>();
        rt.DOKill();
        rt.DOAnchorPos(shownPos, transitionDuration).SetEase(Ease.OutSine);
    }

    public void SemiHide()
    {
        RectTransform rt = display.GetComponent<RectTransform>();
        rt.DOKill();
        rt.DOAnchorPos(semiHiddenPos, transitionDuration*0.5f).SetEase(Ease.InOutSine);
    }

    public void HidePanel()
    {
        RectTransform rt = display.GetComponent<RectTransform>();
        rt.DOKill();
        rt.DOAnchorPos(hiddenPos, transitionDuration).SetEase(Ease.InSine);
    }
}
