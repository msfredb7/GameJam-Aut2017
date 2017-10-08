using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActionsInfo : HeroActions
{
    string name;
    NodeColor color;
    Node destionation;
    bool needNode;

    public HeroActionsInfo(string name, NodeColor color, bool needNode = false)
    {
        this.name = name;
        this.color = color;
        this.needNode = needNode;
        destionation = null;
    }

    public override string GetDisplayName()
    {
        return name;
    }

    public override NodeColor GetNodeColor()
    {
        return color;
    }

    public override void GiveNode(Node node)
    {
        destionation = node;
    }

    public override bool RequiresNode()
    {
        return needNode;
    }
}
