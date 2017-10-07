using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public const string SCENENAME = "TitleScreen";

    public bool exiting = false;

    private bool canExit = false;

    void Start()
    {
        CCC.Manager.MasterManager.Sync(()=> canExit = true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.I))
        {
            Exit();
        }
    }

    public void Exit()
    {
        if (exiting || !canExit)
            return;
        exiting = true;
        LoadingScreen.TransitionTo(LevelSelect.SCENENAME, null);
    }
}
