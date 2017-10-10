using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct ToGameMessage : SceneMessage
{
    public string mapName;
    public bool activateTutorial;
    public ToGameMessage(string mapName, bool activateTutorial)
    {
        this.mapName = mapName;
        this.activateTutorial = activateTutorial;
    }
    public void OnLoaded(Scene scene)
    {
        scene.FindRootObject<GameBootUp>().BootUp(mapName, activateTutorial);
    }

    public void OnOutroComplete()
    {

    }
}
