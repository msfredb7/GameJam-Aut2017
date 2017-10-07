using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LoadingScreenAnimationRemote : MonoBehaviour
{
    public UnityAction onInComplete;
    public UnityAction onOutComplete;
    public Animator controller;


    public void AnimateIn(UnityAction onComplete)
    {
        this.onInComplete = onComplete;
        controller.SetTrigger("In");
    }
    public void AnimateOut(UnityAction onComplete)
    {
        this.onOutComplete = onComplete;
        controller.SetTrigger("Out");
    }
    public void OnInComplete()
    {
        if (onInComplete != null)
            onInComplete();
    }

    public void OnOutComplete()
    {
        if (onOutComplete != null)
            onOutComplete();
    }
}
