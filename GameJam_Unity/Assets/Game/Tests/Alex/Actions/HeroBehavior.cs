using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour {

    [HideInInspector]
    public List<HeroActionEvent> characterActions = new List<HeroActionEvent>();
    protected HeroActionEvent currentAction = null;

    [HideInInspector]
    public List<HeroActionEvent> specialcharacterActions = new List<HeroActionEvent>();
    protected HeroActionEvent currentSpecialAction = null;

    public Action<HeroActionEvent> onAddAction;
    public Action onOrderChange;

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

    public virtual void AddAction(HeroActionEvent newAction)
    {
        characterActions.Add(newAction);
        if (characterActions.Count <= 1)
            readyForNext = true;
        if (onAddAction != null)
            onAddAction.Invoke(characterActions[characterActions.Count - 1]);
    }

    public virtual void MoveAction(HeroActionEvent actionToMove, int position)
    {
        if (position > characterActions.Count || position < 0)
            return;
        characterActions.Insert(position, actionToMove);
        onOrderChange.Invoke();
    }

    public virtual HeroActionEvent GetCurrentAction()
    {
        return currentAction;
    }

    public virtual void AddTemporaryAction(HeroActionEvent newAction)
    {
        specialcharacterActions.Add(newAction);
        if (specialcharacterActions.Count <= 1)
            readyForNext = true;
        if (onAddAction != null)
            onAddAction.Invoke(specialcharacterActions[specialcharacterActions.Count - 1]);
    }

    public virtual void MoveTemporaryAction(HeroActionEvent actionToMove, int position)
    {
        if (position > specialcharacterActions.Count || position < 0)
            return;
        specialcharacterActions.Insert(position, actionToMove);
        onOrderChange.Invoke();
    }

    public virtual HeroActionEvent GetCurrentTemporaryAction()
    {
        return currentSpecialAction;
    }

    #endregion

    #region EXECUTION

    void Update()
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
    protected virtual void ReadyForNextSpecialAction()
    {
        specialcharacterActions.Remove(currentSpecialAction);
        readyForNext = true;
    }


    protected virtual void ExecuteNext()
    {
        if(specialcharacterActions.Count > 0)
        {
            ExecuteNextTemporaryAction();
            return;
        }

        if (characterActions.Count <= characterActions.FindIndex(isCurrentAction) + 1)
        {
            if (looping)
                ExecuteAll();
            return;
        }
        currentAction = characterActions[characterActions.FindIndex(isCurrentAction) + 1];
        currentAction.Execute(hero, ReadyForNextAction);
    }

    protected virtual void ExecuteNextTemporaryAction()
    {
        currentSpecialAction = specialcharacterActions[specialcharacterActions.FindIndex(isCurrentSpecialAction) + 1];
        currentSpecialAction.Execute(hero, ReadyForNextSpecialAction);
    }

    protected virtual bool isCurrentAction(HeroActionEvent action)
    {
        return (action == currentAction);
    }

    protected virtual bool isCurrentSpecialAction(HeroActionEvent action)
    {
        return (action == currentSpecialAction);
    }
    #endregion
}
