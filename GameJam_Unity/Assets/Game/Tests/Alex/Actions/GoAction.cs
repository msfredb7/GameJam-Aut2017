using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAction : HeroActionEvent
{
    GoActionInfo goActionInfo;

    public GoAction()
    {
        goActionInfo = new GoActionInfo();
    }

    public override void Execute(Hero hero, Action onComplete)
    {
        hero.brain.GoToNode(goActionInfo.destination, Brain.Mode.pickup, onComplete);
    }

    public override HeroActions GetHeroActionInfo()
    {
        return goActionInfo;
    }
}
