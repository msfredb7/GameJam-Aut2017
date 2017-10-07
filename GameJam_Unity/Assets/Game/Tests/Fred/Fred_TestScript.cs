using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fred_TestScript : MonoBehaviour
{
    public const string SCENENAME = "Fred_TestScene";

    public Hero hero;
    public Node tNode;
    public Node yNode;
    public Node uNode;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            hero.brain.GoToNode(tNode);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            hero.brain.GoToNode(yNode);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            hero.brain.GoToNode(uNode);
        }
    }
}
