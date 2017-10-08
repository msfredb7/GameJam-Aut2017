using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployAction : HeroActionEvent
{
    DeployActionInfo deployActionInfo;
    private Action myOnComplete;

    public DeployAction()
    {
        deployActionInfo = new DeployActionInfo();
    }

    public override Action OnComplete
    {
        get
        {
            return myOnComplete;
        }
        set
        {
            myOnComplete = value;
        }
    }

    public override void Execute(Hero hero, Action onComplete)
    {
        // deploy
        ZavierZone zavierzone = hero.GetComponent<ZavierZone>();

        if (zavierzone != null)
        {
            zavierzone.zonePreview.enabled = true;
            myOnComplete = delegate ()
            {
                zavierzone.zonePreview.enabled = false;
                onComplete.Invoke();
            };

            zavierzone.onZoneClear += () =>
            {
                ForceCompletion();
                zavierzone.onZoneClear = null;
            };
            zavierzone.activateDeploy = true;
        }
    }

    public override HeroActions GetHeroActionInfo()
    {
        return deployActionInfo;
    }

    public override void PostCloneCleanup()
    {
        deployActionInfo = new DeployActionInfo();
    }
}
