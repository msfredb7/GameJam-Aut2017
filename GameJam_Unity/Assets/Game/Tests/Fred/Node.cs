using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullInspector;

public class Node : BaseBehavior
{
    public List<Node> voisins;
    public int index;


    [InspectorMargin(12), InspectorHeader("Editor")]
    public Node other;

    [InspectorButton]
    public void BuildBidirectionalLink()
    {
        if (other == null)
            return;

        if (!voisins.Contains(other))
            voisins.Add(other);

        if (!other.voisins.Contains(this))
            other.voisins.Add(this);

        other = null;
    }

    [InspectorButton]
    public void ClearLinks()
    {
        for (int i = 0; i < voisins.Count; i++)
        {
            voisins[i].voisins.Remove(this);
        }
        voisins.Clear();
    }

    public Vector2 Position { get { return transform.position; } }

    public static float DistBetween(Node a, Node b)
    {
        return (a.Position - b.Position).magnitude;
    }

    public void OnDrawGizmos()
    {
        if (voisins != null)
            for (int i = 0; i < voisins.Count; i++)
            {
                if (voisins[i] == null)
                {
                    voisins.RemoveAt(i);
                    break;
                }
                Vector2 delta = voisins[i].Position - Position;
                Vector2 offset = delta.Rotate(90).normalized * 0.25f;
                Gizmos.color = new Color(1, 0, 1);
                Gizmos.DrawLine(Position + offset, voisins[i].Position + offset);
            }
    }
}
