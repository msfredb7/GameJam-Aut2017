﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour {

    [HideInInspector]
    public List<CharacterAction> characterActions = new List<CharacterAction>();
    private CharacterAction currentAction = null;

    public Action<CharacterAction> onAddAction;

    public bool looping = false;
    private bool readyForNext = false;

	public void AddAction(CharacterAction.CharacterActionType actionType)
    {
        characterActions.Add(new CharacterAction(actionType));
        if(onAddAction != null)
            onAddAction.Invoke(characterActions[characterActions.Count-1]);
    }

    public void AddAction(CharacterAction newAction)
    {
        characterActions.Add(newAction);
        if (onAddAction != null)
            onAddAction.Invoke(characterActions[characterActions.Count - 1]);
    }

    void Update()
    {
        if (readyForNext)
        {
            readyForNext = false;
            ExecuteNext();
        }
    }

    public void ExecuteAll()
    {
        if (characterActions.Count <= 0)
            return;
        currentAction = characterActions[0];
        currentAction.Execute(ReadyForNextAction);
    }

    private void ReadyForNextAction()
    {
        readyForNext = true;
    }

    private void ExecuteNext()
    {
        int currentActionIndex = characterActions.FindIndex(isCurrentAction);
        if (characterActions.Count <= currentActionIndex + 1)
        {
            if(looping)
                ExecuteAll();
            return;
        }
        CharacterAction newAction = characterActions[currentActionIndex + 1];
        currentAction = newAction;
        newAction.Execute(ExecuteNext);
    }

    private bool isCurrentAction(CharacterAction action)
    {
        return (action == currentAction);
    }
}