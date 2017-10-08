using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployAction : HeroActionEvent
{
    DeployActionInfo deployActionInfo;
    private Action onComplete;

    public DeployAction()
    {
        deployActionInfo = new DeployActionInfo();
    }

    public override Action OnComplete
    {
        get
        {
            return onComplete;
        }
        set
        {
            onComplete = value;
        }
    }

    public override void Execute(Hero hero, Action onComplete)
    {

        // deploy
        ZavierZone zavierzone = hero.GetComponent<ZavierZone>();

        if (zavierzone != null)
        {
            zavierzone.onZoneClear += () =>
            {
                ForceCompletion();
                zavierzone.onZoneClear = null;
            };
            zavierzone.activateDeploy = true;
        }
        else
        {
            Debug.LogError("lol, wtf");
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
