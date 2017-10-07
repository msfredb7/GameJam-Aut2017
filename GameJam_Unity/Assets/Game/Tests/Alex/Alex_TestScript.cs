using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alex_TestScript : MonoBehaviour
{
    public DisplayBehavior display;
    public CharacterBehavior behavior;

    void Start()
    {
        MasterManager.Sync();
        display.Init(behavior);
    }

    public void AddGoNPickup()
    {
        if (behavior != null)
            behavior.AddAction(CharacterAction.CharacterActionType.GoNPickup);
    }

    public void AddGoNDrop()
    {
        if (behavior != null)
            behavior.AddAction(CharacterAction.CharacterActionType.GoNDrop);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (behavior != null)
                behavior.ExecuteAll();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (display != null)
                display.Hide();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (display != null)
                display.Show();
        }
    }
}
