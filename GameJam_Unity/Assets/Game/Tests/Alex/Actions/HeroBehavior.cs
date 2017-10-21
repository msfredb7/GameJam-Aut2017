using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{

    [HideInInspector]
    public List<HeroActionEvent> characterActions = new List<HeroActionEvent>();
    protected HeroActionEvent currentAction = null;

    [HideInInspector]
    public List<HeroActionEvent> temporaryCharacterActions = new List<HeroActionEvent>();
    protected HeroActionEvent currentTemporaryAction = null;

    public Action onListChange;
    public Action onTemporaryListChange;

    public Hero hero;

    public bool looping = false;
    protected bool readyForNext = false;

    public virtual List<HeroActionEvent> GetPossibleActionList()
    {
        return new List<HeroActionEvent>();
    }

    void Start()
    {
        looping = true;
        if (hero == null)
        {
            Hero theHero = GetComponent<Hero>();
            if (theHero != null)
                hero = theHero;
        }
    }

    #region ORDER

    public virtual void RemoveActionFromLoopList(HeroActionEvent theAction)
    {
        theAction.ForceCompletion();
        characterActions.Remove(theAction);

        if (onListChange != null)
            onListChange.Invoke();
    }
    public virtual void RemoveActionFromTemporaryList(HeroActionEvent theAction)
    {
        theAction.ForceCompletion();
        temporaryCharacterActions.Remove(theAction);

        if (onTemporaryListChange != null)
            onTemporaryListChange();
    }

    public virtual void MoveAction(HeroActionEvent actionToMove, int position)
    {
        if (position > characterActions.Count || position < 0)
            return;
        characterActions.Insert(position, actionToMove);

        if (onListChange != null)
            onListChange.Invoke();
    }

    public virtual void MoveTemporaryAction(HeroActionEvent actionToMove, int position)
    {
        if (position > temporaryCharacterActions.Count || position < 0)
            return;
        temporaryCharacterActions.Insert(position, actionToMove);

        if (onTemporaryListChange != null)
            onTemporaryListChange.Invoke();
    }

    public virtual HeroActionEvent GetCurrentAction()
    {
        return currentAction;
    }

    public virtual void AddActionAt(HeroActionEvent newAction, int index)
    {
        characterActions.Insert(index, newAction);
        if (characterActions.Count <= 1)
            readyForNext = true;

        if (onListChange != null)
            onListChange.Invoke();
    }

    public virtual void AddTemporaryActionAt(HeroActionEvent newAction, int index)
    {
        //...
        //print("insert at: " + index);
        temporaryCharacterActions.Insert(index, newAction);

        if (temporaryCharacterActions.Count <= 1)
            readyForNext = true;

        if (index == 0)
        {
            readyForNext = true;
            ExecuteNextTemporaryAction();
        }

        if (onTemporaryListChange != null)
            onTemporaryListChange.Invoke();
    }

    public virtual void AddAction(HeroActionEvent newAction)
    {
        characterActions.Add(newAction);
        if (characterActions.Count <= 1)
            readyForNext = true;

        if (onListChange != null)
            onListChange.Invoke();
    }

    public virtual void AddTemporaryAction(HeroActionEvent newAction)
    {
        temporaryCharacterActions.Add(newAction);
        if (temporaryCharacterActions.Count <= 1)
            readyForNext = true;

        if (onTemporaryListChange != null)
            onTemporaryListChange();
    }

    public virtual HeroActionEvent GetCurrentTemporaryAction()
    {
        return currentTemporaryAction;
    }

    #endregion

    #region EXECUTION

    void FixedUpdate()
    {
        if (readyForNext)
        {
            readyForNext = false;
            ExecuteNext();
        }
    }

    public virtual void ExecuteAll()
    {
        if (characterActions.Count <= 0)
            return;
        currentAction = characterActions[0];
        currentAction.Execute(hero, ReadyForNextAction);
    }

    protected virtual void ReadyForNextAction()
    {
        readyForNext = true;
    }
    protected virtual void ReadyForNextTemporaryAction()
    {
        temporaryCharacterActions.Remove(currentTemporaryAction);
        if (onTemporaryListChange != null)
            onTemporaryListChange();
        readyForNext = true;
        if (currentAction != null)
            currentAction.ForceCompletion();
    }


    protected virtual void ExecuteNext()
    {
        if (temporaryCharacterActions.Count > 0)
        {
            ExecuteNextTemporaryAction();
            return;
        }

        if (characterActions.Count <= characterActions.FindIndex(IsCurrentAction) + 1)
        {
            if (looping)
                ExecuteAll();
            return;
        }
        currentAction = characterActions[characterActions.FindIndex(IsCurrentAction) + 1];
        currentAction.Execute(hero, ReadyForNextAction);
    }

    protected virtual void ExecuteNextTemporaryAction()
    {
        if (temporaryCharacterActions.Count < 1)
            return;

        currentTemporaryAction = temporaryCharacterActions[0];
        currentTemporaryAction.Execute(hero, ReadyForNextTemporaryAction);
    }

    protected virtual bool IsCurrentAction(HeroActionEvent action)
    {
        return (action == currentAction);
    }

    protected virtual bool IsCurrentSpecialAction(HeroActionEvent action)
    {
        return (action == currentTemporaryAction);
    }
    #endregion
}
