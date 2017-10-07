using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct ToGameMessage : SceneMessage
{
    public string mapName;
    public ToGameMessage(string mapName)
    {
        this.mapName = mapName;
    }
    public void OnLoaded(Scene scene)
    {
        scene.FindRootObject<GameBootUp>().BootUp(mapName);
    }

    public void OnOutroComplete()
    {

    }
}
