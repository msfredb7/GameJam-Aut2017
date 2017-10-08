using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBD_Templates : MonoBehaviour
{
    [Header("Links")]
    public RectTransform stdContainer;
    public RectTransform specialContainer;

    [Header("Prefabs")]
    public HBD_TemplateAction templatePrefab;

    public Action<HBD_TemplateAction> onNewActionClick;

    public void Fill()
    {
        //Clear existing items

        //Spawn new items
    }
}
