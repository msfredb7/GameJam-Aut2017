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

    public virtual void Execute(Action onComplete)
    {
        switch (actionType)
        {
            case CharacterActionType.GoNPickup:
                "Go and Pickup".LogWarning();
                onComplete.Invoke();
                break;
            case CharacterActionType.GoNDrop:
                "Go and Drop".LogWarning();
                onComplete.Invoke();
                break;
            default:
                break;
        }
    }
}
