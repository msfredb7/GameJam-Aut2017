using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class TutorialUI : MonoBehaviour {

    public GameBootUp gameBootUp;
    public Spotlight spotlight;
    public TutorialIndications tutorialMessage;
    public GameObject inputBlocker;

    public RectTransform heroPanel;
    public RectTransform statsPanelLeft;
    public RectTransform statsPanelRight;
    public RectTransform statsPanelMid;
    public RectTransform hireHeroesButton;

    public bool activateTutorial = false;

    public float textReadTime = 5;

    void Start()
    {
        if (activateTutorial)
        {
            Game.OnGameStart += DoTutorial;
        } else
        {
            inputBlocker.SetActive(false);
        }
    }

    void ShowUI(Vector2 position, string description, string title, Action onComplete)
    {
        tutorialMessage.Hide(delegate ()
        {
            spotlight.On(position, delegate ()
            {
                tutorialMessage.Show(delegate ()
                {
                    DelayManager.LocalCallTo(onComplete, textReadTime, this, true);
                }, description, title);
            });
        });
    }

    void DoTutorial()
    {
        Time.timeScale = 0;
        inputBlocker.SetActive(true);
        ShowUI(heroPanel.position, "HERO PANEL", "This is the hero you currently selected, click on his portrait " +
            "to open the Hero Options menu. You can also make the camera follow him or select another hero.", delegate ()
        {
            ShowUI(statsPanelLeft.position, "CASH DISPLAY", "This is the money you currently have. Use it to hire heroes" +
                " or keep it to make your way to the objective.", delegate ()
            {
                ShowUI(statsPanelMid.position, "TIME REMAINING", "You have a certain time remaining until you fail the objective." +
                    " Always keep an eye on this timer!", delegate ()
                {
                    ShowUI(statsPanelRight.position, "CURRENT OBJECTIVE", "Your goal is to make that much profit with your delivaries." +
                        " Optimize your delivary paterns to be more efficient.", delegate ()
                    {
                        ShowUI(hireHeroesButton.position, "HIRE HEROES", "Be more efficient by getting yourself more "+
                            " heroes. It's a little cost now for a big revenue later", delegate ()
                        {
                            spotlight.Off();
                            tutorialMessage.Hide(null);
                            Time.timeScale = 1;
                            inputBlocker.SetActive(false);
                        });
                    });
                });
            });
        });
    }
}
