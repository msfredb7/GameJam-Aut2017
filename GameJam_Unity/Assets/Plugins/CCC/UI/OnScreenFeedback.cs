﻿using CCC.Manager;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenFeedback : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 0.25f;
    public int bounceCount =3;
    [ReadOnly]
    public float totalDuration;

    private Tween showAnimation;

    public void BounceAnim(Sprite sprite, Action onComplete)
    {
        image.sprite = sprite;

        Sequence sq = DOTween.Sequence();
        sq.Join(
            image.DOFade(1, fadeDuration));
        sq.Append(
            image.gameObject.transform.DOScale(1.25f, 0.25f).SetLoops(bounceCount*2, LoopType.Yoyo));
        sq.Append(image.DOFade(0, fadeDuration));
        sq.OnComplete(delegate ()
        {
            image.SetAlpha(0);
            image.gameObject.transform.localScale = new Vector3(1, 1, 1);
            onComplete();
        });
    }

    private void OnValidate()
    {
        totalDuration = fadeDuration + (0.25f * (bounceCount * 2)) + fadeDuration;
    }
}
