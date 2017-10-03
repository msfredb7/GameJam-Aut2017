using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fred_TestScript : MonoBehaviour
{
    public const string SCENENAME = "Fred_TestScene";

    public void Go()
    {
        LoadingScreen.TransitionTo(GameBuilder.SCENENAME, new ToGameMessage(), true);
    }
}
