using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fred_TestScript : MonoBehaviour
{
    public const string SCENENAME = "Fred_TestScene";

    public Node start;
    public Node destination;

    public FAStar algomagie;

    public PathOfDoom path;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Go();
        }
    }

    public void Go()
    {
        algomagie.ApplyIndexes();
        path = algomagie.CalculatePath(start, destination);
    }

    void OnDrawGizmos()
    {
        if (path == null)
        {
        }
        else
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < path.nodes.Count - 1; i++)
            {
                Gizmos.DrawLine(path.nodes[i].Position, path.nodes[i + 1].Position);
            }
        }
    }
}
