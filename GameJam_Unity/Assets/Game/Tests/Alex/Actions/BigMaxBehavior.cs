using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMaxBehavior : HeroBehavior {

    private List<HeroActionEvent> possibleActions = new List<HeroActionEvent>();

    public override List<HeroActionEvent> GetPossibleActionList()
    {
        possibleActions.Add(new GoAction());
        possibleActions.Add(new DropAction());
        return possibleActions;
    }
}
