using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBuilder : MonoBehaviour
{
    public const string SCENENAME = "GameBuilder";

    int waitingToLoadCount = 0;

    private GameUI gameUI;

    public void Build()
    {
        Debug.Log("Building game ...");
        waitingToLoadCount = 1;

        string sceneName = GameUI.SCENENAME;

        // Load All Scenes
        if (!Scenes.Exists(sceneName))
            Scenes.LoadAsync(sceneName, LoadSceneMode.Additive, OnUILoaded);
        else
            OnUILoaded(Scenes.GetActive(sceneName));
    }

    void OnUILoaded(Scene scene)
    {
        gameUI = scene.FindRootObject<GameUI>();
        //...

        waitingToLoadCount--;
        CheckInitGame();
    }

    void CheckInitGame()
    {
        if (waitingToLoadCount <= 0)
        {
            Debug.Log("Game built");
            Game.instance.PrepareLaunch(gameUI);
        }
    }
}
