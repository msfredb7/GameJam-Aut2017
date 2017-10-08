using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMaxBehavior : HeroBehavior {

    private List<HeroActionEvent> possibleActions = new List<HeroActionEvent>();

    void Awake()
    {
        possibleActions.Add(new GoAction());
        possibleActions.Add(new DropAction());
    }

    public override List<HeroActionEvent> GetPossibleActionList()
    {
        return possibleActions;
    }
}
