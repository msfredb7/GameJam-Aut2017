using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction {

    public enum CharacterActionType { GoNPickup = 0, GoNDrop = 1 }

    public CharacterActionType actionType;

    public CharacterAction(CharacterActionType actionType)
    {
        this.actionType = actionType;
    }

    public virtual void Execute(Hero hero, Action onComplete)
    {
        if (hero == null)
            return;
        switch (actionType)
        {
            case CharacterActionType.GoNPickup:
                List<Node> nodes = hero.brain.state.GetNextOrStayNode().voisins;
                hero.brain.GoToNode(nodes[UnityEngine.Random.Range(0, nodes.Count)]);
                onComplete.Invoke();
                break;
            case CharacterActionType.GoNDrop:
                hero.brain.GoToNode(nodes[UnityEngine.Random.Range(0, nodes.Count)]);
                onComplete.Invoke();
                break;
            default:
                break;
        }
    }
}
