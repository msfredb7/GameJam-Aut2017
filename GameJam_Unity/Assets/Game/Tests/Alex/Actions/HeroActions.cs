using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroActions
{
    public enum NodeColor { Red, Yellow, Green, White, Blue }

    public abstract string GetDisplayName();
    public abstract NodeColor GetNodeColor();
    public abstract bool RequiresNode();
    public abstract void GiveNode(Node node);
    public abstract bool IsUnique();
}
