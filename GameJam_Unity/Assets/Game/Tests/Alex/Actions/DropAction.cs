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
        if (onComplete != null)
            onComplete();
        onComplete = null;
    }

    public override HeroActions GetHeroActionInfo()
    {
        return dropActionInfo;
    }
    public override Action OnComplete
    {
        get
        {
            return null;
        }
        set {}
    }

    public override void PostCloneCleanup()
    {
        dropActionInfo = new DropActionInfo();
    }
}
