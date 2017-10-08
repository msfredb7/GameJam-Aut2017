using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZavierBehavior : HeroBehavior
{
    private List<HeroActionEvent> possibleActions = new List<HeroActionEvent>();

    void Awake()
    {
        possibleActions.Add(new GoAction());
        possibleActions.Add(new DropAction());
        possibleActions.Add(new DeployAction());
    }

    public override List<HeroActionEvent> GetPossibleActionList()
    {
        return possibleActions;
    }
}
