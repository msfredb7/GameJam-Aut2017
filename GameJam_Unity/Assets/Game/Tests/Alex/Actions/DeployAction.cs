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

    public override void Execute(Hero hero, Action onComplete)
    {
        // deploy
        ZavierZone zavierzone = hero.GetComponent<ZavierZone>();

        if (zavierzone != null)
        {
            zavierzone.onZoneClear += onComplete;
            zavierzone.activateDeploy = true;
        }
    }

    public override HeroActions GetHeroActionInfo()
    {
        return deployActionInfo;
    }
}
