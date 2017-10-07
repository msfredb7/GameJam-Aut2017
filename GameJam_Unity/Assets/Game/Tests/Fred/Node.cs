using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullInspector;

public class Node : BaseBehavior
{
    public class HeroTransition
    {
        public Node from;
        public Node to;
        public Hero theHero;
        public void Flip()
        {
            Node lastFrom = from;
            from = to;
            to = lastFrom;
        }

        public void NoticeDelete()
        {
            from.heroTransitions.Remove(this);
            to.heroTransitions.Remove(this);
            theHero.brain.state.transition = null;
        }
        public void NoticeCreate()
        {
            from.heroTransitions.Add(this);
            to.heroTransitions.Add(this);
            theHero.brain.state.transition = this;
        }
    }
    public List<Node> voisins;
    public int index;

    public List<HeroTransition> heroTransitions = new List<HeroTransition>();

    public Vector2 Position { get { return transform.position; } }

    public static float DistBetween(Node a, Node b)
    {
        return (a.Position - b.Position).magnitude;
    }

    public bool IsLinkAvailable(Node a, Node b)
    {
        int bidon = 0;
        return IsLinkAvailable(a, b, out bidon);
    }
    public bool IsLinkAvailable(Node a, Node b, out int transitionIndex)
    {
        transitionIndex = 0;
        foreach (var transition in a.heroTransitions)
        {
            if (transition.from == b || transition.to == b)
                return false;
            transitionIndex++;
        }
        transitionIndex = -1;
        return true;
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

        other.SaveState();
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
}
