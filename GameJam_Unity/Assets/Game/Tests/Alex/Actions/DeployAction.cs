using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployAction : HeroActionEvent
{
    DeployActionInfo deployActionInfo;

    public DeployAction()
    {
        deployActionInfo = new DeployActionInfo();
    }

    public override Action OnComplete
    {
        get
        {
            return null;
        }
    }

    public override void Execute(Hero hero, Action onComplete)
    {
        // deploy
        Debug.Log("executing a deploy action");
        if (onComplete != null)
            onComplete();
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
