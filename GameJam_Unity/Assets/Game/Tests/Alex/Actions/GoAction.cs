using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAction : HeroActionEvent
{
    GoActionInfo goActionInfo;

    Action onComplete;
    Hero hero;
    float id;

    public GoAction()
    {
        goActionInfo = new GoActionInfo();
        id = UnityEngine.Random.Range(0, 500);
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
        this.hero = hero;

        Debug.Log("executing a go action...");
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
        hero.brain.GoToNode(goActionInfo.destination, Brain.Mode.pickup, onComplete);
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
        id = UnityEngine.Random.Range(0, 500);
    }
}
