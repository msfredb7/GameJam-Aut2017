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
        Debug.Log("Drop tha pizza!");
    }

    public override HeroActions GetHeroActionInfo()
    {
        return dropActionInfo;
    }
}
