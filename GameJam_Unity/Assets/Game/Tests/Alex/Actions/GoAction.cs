using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAction : HeroActionEvent
{
    GoActionInfo goActionInfo;

    Action onComplete;

    public GoAction()
    {
        goActionInfo = new GoActionInfo();
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
        Debug.Log("executing a go action...");
        //hero.brain.GoToNode(goActionInfo.destination, Brain.Mode.pickup, onComplete);
    }

    public override HeroActions GetHeroActionInfo()
    {
        return goActionInfo;
    }
}
