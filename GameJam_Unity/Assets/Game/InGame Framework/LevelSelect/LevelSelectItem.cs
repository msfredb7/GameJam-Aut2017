using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectItem : MonoBehaviour
{
    public string sceneName;

    public void LaunchGameLevel1()
    {
        LoadingScreen.TransitionTo(GameBuilder.SCENENAME, new ToGameMessage(sceneName,true), true);
    }

    public void LaunchGameLevel2()
    {
        LoadingScreen.TransitionTo(GameBuilder.SCENENAME, new ToGameMessage(sceneName,false), true);
    }

    public void LaunchGameLevel3()
    {
        LoadingScreen.TransitionTo(GameBuilder.SCENENAME, new ToGameMessage(sceneName,false), true);
    }
}
