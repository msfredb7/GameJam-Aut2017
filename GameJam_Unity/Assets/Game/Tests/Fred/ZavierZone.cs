using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZavierZone : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();

    public SpriteRenderer zonePreview;

    public Action remoteUpdater;

    void Update()
    {
        if(remoteUpdater != null)
        {
            remoteUpdater();
        }
    }

    public Node GetClosestNodeWithOrder()
    {
        Node closestNode = null;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Order != null)
            {
                if (closestNode == null)
                {
                    closestNode = nodes[i];
                }
                else
                {
                    if (Vector3.Distance(nodes[i].Position, transform.position) <
                        Vector3.Distance(closestNode.Position, transform.position))
                    {
                        closestNode = nodes[i];
                    }
                }
            }
        }
        return closestNode;
    }

    public Node GetClosestNodeWithPizza()
    {
        Node closestNode = null;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].pizza.Count > 0)
            {
                if (closestNode == null)
                {
                    closestNode = nodes[i];
                }
                else
                {
                    if (Vector3.Distance(nodes[i].Position, transform.position) <
                        Vector3.Distance(closestNode.Position, transform.position))
                    {
                        closestNode = nodes[i];
                    }
                }
            }
        }
        return closestNode;
    }
}
