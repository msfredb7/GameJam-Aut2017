using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Notification
{
    public string text;
    public Action onShow;
    public Action onHide;
}
