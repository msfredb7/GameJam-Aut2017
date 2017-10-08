using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : HeroActionEvent
{
    DropActionInfo dropActionInfo;

    private Action onComplete;

    public DropAction()
    {
        dropActionInfo = new DropActionInfo();
    }

    public override Action OnComplete
    {
        get
        {
            return onComplete;
        }
    }

    public override void Execute(Hero hero, Action onComplete)
    {
        this.onComplete = onComplete;
        Debug.Log("Drop tha pizza!");
    }

    public override HeroActions GetHeroActionInfo()
    {
        return dropActionInfo;
    }
}
