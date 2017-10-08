using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Utility;

public abstract class HeroActionEvent
{
    public abstract void Execute(Hero hero, Action onComplete);
    public abstract HeroActions GetHeroActionInfo();
    public abstract Action OnComplete { get; }
    public abstract void PostCloneCleanup();

    public HeroActionEvent Clone()
    {
        HeroActionEvent clone = (MemberwiseClone() as HeroActionEvent);
        clone.PostCloneCleanup();
        return clone;
    }

    public void ForceCompletion()
    {
        if (OnComplete != null)
            OnComplete();
    }
}
