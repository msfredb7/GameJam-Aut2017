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
    public GameObject DeliverNotificationObject;
    public FeedbackDisplay FeedbackDisplay;

    public HeroPortrait portrait;

    private void Start()
    {
        CCC.Manager.MasterManager.Sync();
    }
}
