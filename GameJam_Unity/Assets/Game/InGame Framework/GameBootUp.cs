using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootUp : MonoBehaviour
{
    public string mapName = "Map1";
    public bool activateTutorial;

    void Start()
    {
        if (SceneManager.sceneCount != 1)
            return;

        MasterManager.Sync(DebugStartGame);
    }

    void DebugStartGame()
    {
        if (gameObject.scene.name == GameBuilder.SCENENAME)
        {
            BootUp(mapName, false);
            return;
        }

        Scenes.LoadAsync(GameBuilder.SCENENAME, LoadSceneMode.Additive, OnGameLoaded);
    }

    private void OnGameLoaded(Scene scene)
    {
        Debug.Log("Game boot up");
        scene.FindRootObject<GameBuilder>().Build(mapName, activateTutorial);
    }

    public void BootUp(string mapName, bool activateTutorial)
    {
        this.mapName = mapName;
        this.activateTutorial = activateTutorial;
        OnGameLoaded(gameObject.scene);
    }
}
