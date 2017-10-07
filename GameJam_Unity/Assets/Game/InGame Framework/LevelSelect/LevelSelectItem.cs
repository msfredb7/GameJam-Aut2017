using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectItem : MonoBehaviour
{
    public string sceneName;

    public void LaunchGame()
    {
        LoadingScreen.TransitionTo(GameBuilder.SCENENAME, new ToGameMessage(sceneName), true);
    }
}
