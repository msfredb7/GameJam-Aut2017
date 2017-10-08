using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : HeroActionEvent
{
    DropActionInfo dropActionInfo;

    public DropAction()
    {
        dropActionInfo = new DropActionInfo();
    }

    public override void Execute(Hero hero, Action onComplete)
    {
        hero.Drop();
    }

    public override HeroActions GetHeroActionInfo()
    {
        return dropActionInfo;
    }
}
