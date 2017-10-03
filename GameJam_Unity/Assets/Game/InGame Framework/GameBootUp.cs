using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootUp : MonoBehaviour
{

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
            BootUp();
            return;
        }

        Scenes.LoadAsync(GameBuilder.SCENENAME, LoadSceneMode.Additive, OnGameLoaded);
    }

    private void OnGameLoaded(Scene scene)
    {
        Debug.Log("Game boot up");
        scene.FindRootObject<GameBuilder>().Build();
    }

    public void BootUp()
    {
        OnGameLoaded(gameObject.scene);
    }
}
