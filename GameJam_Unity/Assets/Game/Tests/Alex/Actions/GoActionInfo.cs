using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoActionInfo : HeroActions
{
    public Node destination;

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
