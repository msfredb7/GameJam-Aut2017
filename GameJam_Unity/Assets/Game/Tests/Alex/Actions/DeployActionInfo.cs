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
        return NodeColor.White;
    }

    public override void GiveNode(Node node)
    {
        throw new NotImplementedException();
    }

    public override bool RequiresNode()
    {
        throw new NotImplementedException();
    }
}
