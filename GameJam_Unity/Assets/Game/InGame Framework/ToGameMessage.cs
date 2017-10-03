using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct ToGameMessage : SceneMessage
{
    public void OnLoaded(Scene scene)
    {
        scene.FindRootObject<GameBootUp>().BootUp();
    }

    public void OnOutroComplete()
    {

    }
}
