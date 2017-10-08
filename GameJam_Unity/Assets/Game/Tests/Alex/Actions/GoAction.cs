using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAction : HeroActionEvent
{
    GoActionInfo goActionInfo;

    Action onComplete;
    Hero hero;

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
        set
        {
            onComplete = value;
        }
    }

    public override void Execute(Hero hero, Action onComplete)
    {
        this.onComplete = onComplete;
        this.hero = hero;

        //Debug.Log("executing a go action...");
        if (goActionInfo.destination == null)
        {
            goActionInfo.onNodeGiven = GoToDestination;
        }
        else
        {
            GoToDestination();
        }
    }

    private void GoToDestination()
    {
        hero.brain.GoToNode(goActionInfo.destination, Brain.Mode.pickup, ForceCompletion);
    }

    public override HeroActions GetHeroActionInfo()
    {
        return goActionInfo;
    }

    public override void PostCloneCleanup()
    {
        goActionInfo = new GoActionInfo();
        onComplete = null;
        hero = null;
    }
}
