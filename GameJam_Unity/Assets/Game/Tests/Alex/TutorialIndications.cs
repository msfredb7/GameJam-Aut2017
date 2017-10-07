using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CCC.UI;

public class TutorialIndications : MonoBehaviour {

    public CanvasGroup canvas;
    public Text title;
    public Text description;
    public WindowAnimation windowAnim;

	public void Show(Action onComplete, string title, string description)
    {
        SetMessage(title, description);
        if (windowAnim != null)
            windowAnim.Open(delegate() {
                if (onComplete != null)
                    onComplete();
            });
    }

    public void Hide(Action onComplete)
    {
        if (windowAnim != null)
            windowAnim.Close(delegate () {
                if (onComplete != null)
                    onComplete();
            });
    }

    public void SetMessage(string title, string description)
    {
        this.title.text = title;
        this.description.text = description;
    }
}
