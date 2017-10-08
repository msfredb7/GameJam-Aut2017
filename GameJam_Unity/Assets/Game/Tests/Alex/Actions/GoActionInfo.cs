using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoActionInfo : HeroActions
{
    public Node destination;
    public Action onNodeGiven;

    public override string GetDisplayName()
    {
        return "Go";
    }

    public override NodeColor GetNodeColor()
    {
        return NodeColor.Red;
    }

    public override void GiveNode(Node node)
    {
        destination = node;
        if (onNodeGiven != null)
            onNodeGiven();
    }

    public override bool IsUnique()
    {
        return false;
    }

    public override bool RequiresNode()
    {
        return true;
    }
}
