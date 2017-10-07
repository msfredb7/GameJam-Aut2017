using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreenAnimation : MonoBehaviour {

    public Image bg;
    public Camera cam;

    public LoadingScreenAnimationRemote remote;

    public void Intro(UnityAction onComplete)
    {
        remote.AnimateIn(()=>
        {
            Camera.main.gameObject.SetActive(false);
            cam.gameObject.SetActive(true);
            onComplete();
        });
        //bg.DOFade(1, 1).OnComplete(delegate ()
        //{
        //    Camera.main.gameObject.SetActive(false);
        //    cam.gameObject.SetActive(true);
        //    onComplete();
        //}).SetUpdate(true);
    }

    public void Outro(UnityAction onComplete)
    {
        cam.gameObject.SetActive(false);
        remote.AnimateOut(()=>
        {
            onComplete();
        });
        //bg.DOFade(0, 1).OnComplete(delegate ()
        //{
        //    onComplete();
        //}).SetUpdate(true);
    }

    public void OnNewSceneLoaded()
    {
        cam.gameObject.SetActive(false);
    }
}
