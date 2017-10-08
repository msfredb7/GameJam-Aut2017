using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropActionInfo : HeroActions
{
    public override string GetDisplayName()
    {
        return "Drop";
    }

    public override NodeColor GetNodeColor()
    {
        return NodeColor.Blue;
    }

    public override void GiveNode(Node node)
    {
    }

    public override bool IsUnique()
    {
        return false;
    }

    public override bool RequiresNode()
    {
        return false;
    }
}
