using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroActionEvent {

    public abstract void Execute(Hero hero, Action onComplete);
    public abstract HeroActions GetHeroActionInfo();
}
