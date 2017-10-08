using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployActionInfo : HeroActions
{
    public override string GetDisplayName()
    {
        return "Deploy";
    }

    public override NodeColor GetNodeColor()
    {
        return NodeColor.Yellow;
    }

    public override void GiveNode(Node node)
    {
        
    }

    public override bool IsUnique()
    {
        return true;
    }

    public override bool RequiresNode()
    {
        return false;
    }
}
