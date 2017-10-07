using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction {

    public enum CharacterActionType { GoNPickup = 0, GoNDrop = 1 }

    public CharacterActionType actionType;

    public Action onComplete;

    public CharacterAction(CharacterActionType actionType)
    {
        this.actionType = actionType;
    }

    public virtual void Execute(Hero hero, Action onComplete)
    {
        if (hero == null)
            return;
        this.onComplete = onComplete;
        switch (actionType)
        {
            case CharacterActionType.GoNPickup:
                Debug.Log("Go And Pickup");
                List<Node> nodes = hero.brain.state.GetNextOrStayNode().voisins;
                hero.brain.GoToNode(nodes[UnityEngine.Random.Range(0, nodes.Count)], OnDestinationReached);
                break;
            case CharacterActionType.GoNDrop:
                Debug.Log("Go And Drop");
                hero.brain.GoToNode(hero.currentNode, OnDestinationReached);
                break;
            default:
                break;
        }
    }

    public void OnDestinationReached()
    {
        onComplete.Invoke();
    }
}
