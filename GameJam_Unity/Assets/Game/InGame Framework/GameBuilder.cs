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
    private Map map;

    public void Build(string mapName)
    {
        Debug.Log("Building game ...");
        waitingToLoadCount = 2;

        string sceneName = GameUI.SCENENAME;

        // Load All Scenes
        if (!Scenes.Exists(sceneName))
            Scenes.LoadAsync(sceneName, LoadSceneMode.Additive, OnUILoaded);
        else
            OnUILoaded(Scenes.GetActive(sceneName));


        if (!Scenes.Exists(mapName))
            Scenes.LoadAsync(mapName, LoadSceneMode.Additive, OnUILoaded);
        else
            OnMapLoaded(Scenes.GetActive(mapName));
    }

    void OnUILoaded(Scene scene)
    {
        gameUI = scene.FindRootObject<GameUI>();
        //...

        waitingToLoadCount--;
        CheckInitGame();
    }
    void OnMapLoaded(Scene scene)
    {
        map = scene.FindRootObject<Map>();
        //...

        waitingToLoadCount--;
        CheckInitGame();
    }

    void CheckInitGame()
    {
        if (waitingToLoadCount <= 0)
        {
            Debug.Log("Game built");
            Game.instance.PrepareLaunch(gameUI, map);
        }
    }
}
