using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HBD_Action : HBD_Button
{
    public Text displayName;
    public float minDragSQRPixel = 25;

    public Action<HBD_Action> onDeleteClick;
    public Action<HBD_Action> onDragOut;

    [ReadOnly]
    public bool isDragging = false;
    [ReadOnly]
    public Vector2 dragStart;

    public void DeleteClick()
    {
        if (onDeleteClick != null)
            onDeleteClick(this);
    }

    public void DragBegin()
    {
        dragStart = Input.mousePosition;
        isDragging = true;
    }

    public void DragEnd()
    {
        isDragging = false;
    }

    public void DragOut()
    {
        if (onDragOut != null)
            onDragOut(this);
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            if(((Vector2)Input.mousePosition - dragStart).sqrMagnitude > minDragSQRPixel)
            {
                DragOut();
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowAndFill(HeroActions action)
    {
        gameObject.SetActive(true);

        //Remplir information
    }
}
