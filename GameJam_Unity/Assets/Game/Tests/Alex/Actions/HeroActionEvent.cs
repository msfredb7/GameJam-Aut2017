using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroActionEvent
{
    public abstract void Execute(Hero hero, Action onComplete);
    public abstract HeroActions GetHeroActionInfo();
    public abstract Action OnComplete { get; }

    public HeroActionEvent Clone() { return (HeroActionEvent)MemberwiseClone(); }

    public void ForceCompletion()
    {
        if (OnComplete != null)
            OnComplete();
    }
}
