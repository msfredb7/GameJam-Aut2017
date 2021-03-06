using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public const string SCENENAME = "GameUI";

    public Action onHeroSelected;

    public HeroShop heroShop;
    public ObjectiveDisplay objectiveDisplay;
    public DeliveryNotification DeliverNotificationObject;
    public FeedbackDisplay FeedbackDisplay;
    public TutorialUI tutorial;

    public HeroPortrait portrait;

    private void Start()
    {
        CCC.Manager.MasterManager.Sync();
    }

    public void SetTutorialActive(bool activate)
    {
        tutorial.activateTutorial = activate;
    }
}
