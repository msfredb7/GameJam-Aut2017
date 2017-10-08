using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction {

    public enum CharacterActionType { GoNPickup = 0, GoNDrop = 1 }

    public CharacterActionType actionType;

    public Action onComplete;

    public Node destination;


    public CharacterAction(CharacterActionType actionType, Node destination)
    {
        this.actionType = actionType;
        this.destination = destination;
        onComplete = null;
    }



    public virtual void Execute(Hero hero, Action onComplete)
    {
        if (hero == null)
            return;
        this.onComplete = onComplete;
        switch (actionType)
        {
            case CharacterActionType.GoNPickup:
                hero.brain.GoToNode(destination, Brain.Mode.pickup, OnDestinationReached);
                break;
            case CharacterActionType.GoNDrop:
                //hero.brain.GoToNode(destination, Brain.Mode.drop, OnDestinationReached);
                break;
            default:
                break;
        }
    }

    public void OnDestinationReached()
    {
        if(onComplete != null)
            onComplete.Invoke();
    }
}
