using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullInspector;

public class FAStar : BaseBehavior
{
    public List<Node> nodes;

    [InspectorButton]
    public void GatherAllNodes()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Node");
        nodes.Clear();
        nodes.Capacity = objs.Length;

        for (int i = 0; i < objs.Length; i++)
        {
            nodes.Add(objs[i].GetComponent<Node>());
        }

    }

    public void ApplyIndexes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].index = i;
        }
    }

    public PathOfDoom CalculatePath(Node start, Node goal)
    {
        List<Node> closedSet = new List<Node>();

        List<Node> openSet = new List<Node>();
        openSet.Add(start);

        Node[] cameFrom = new Node[nodes.Count];

        float[] gScore = new float[nodes.Count];
        gScore.Populate(float.PositiveInfinity);
        gScore[start.index] = 0;

        float[] fScore = new float[nodes.Count];
        fScore.Populate(float.PositiveInfinity);

        fScore[start.index] = CostEstimate(start, goal);

        while (openSet.Count > 0)
        {
            Node current = GetCheapestNodeFromSet(openSet, fScore);

            if (current == goal)
                return Reconstruct_path(cameFrom, current);


            openSet.Remove(current);
            closedSet.Add(current);

            for (int i = 0; i < current.voisins.Count; i++)
            {
                Node voisin = current.voisins[i];
                int voisinIndex = voisin.index;

                if (closedSet.Contains(voisin))
                    continue;

                if (!openSet.Contains(voisin))
                    openSet.Add(voisin);

                float distanceToVoisin = Node.DistBetween(current, voisin);

                float tentative_gScore = gScore[current.index] + distanceToVoisin;

                if (tentative_gScore >= gScore[voisinIndex])
                    continue;


                cameFrom[voisinIndex] = current;
                gScore[voisinIndex] = tentative_gScore;
                fScore[voisinIndex] = gScore[voisinIndex] + CostEstimate(voisin, goal);
            }

        }
        return null;
    }

    private Node GetCheapestNodeFromSet(List<Node> openSet, float[] costs)
    {
        Node recordHolder = null;
        float record = float.PositiveInfinity;
        for (int i = 0; i < openSet.Count; i++)
        {
            float cost = costs[openSet[i].index];
            if (cost < record)
            {
                recordHolder = openSet[i];
                record = cost;
            }
        }
        return recordHolder;
    }

    private float CostEstimate(Node a, Node b)
    {
        return (a.Position - b.Position).magnitude;
    }

    private PathOfDoom Reconstruct_path(Node[] cameFrom, Node current)
    {
        PathOfDoom total_path = new PathOfDoom();

        while (current != null)
        {
            total_path.nodes.Add(current);
            current = cameFrom[current.index];
        }
        return total_path;
    }
}

public class PathOfDoom
{
    public List<Node> nodes = new List<Node>();
    public Node GetNext()
    {
        return nodes[nodes.Count - 1];
    }
    public void RemoveClosest()
    {
        nodes.RemoveAt(nodes.Count - 1);
    }
}
