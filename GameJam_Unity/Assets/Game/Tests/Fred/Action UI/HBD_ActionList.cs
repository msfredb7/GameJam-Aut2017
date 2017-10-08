using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBD_ActionList : MonoBehaviour
{
    [Header("Links")]
    public RectTransform container;
    public PointerListener pointerListener;

    [Header("Prefab")]
    public HBD_Action actionItemPrefab;
    public HBD_EmptySpot emptySpotPrefab;

    [Header("Dynamic list"), ReadOnly]
    public List<HBD_Action> actionItems = new List<HBD_Action>();

    public Action<HBD_Action> onDeleteActionClick;
    public Action<HBD_Action> onDragOut;

    public void Fill(List<HeroActionEvent> actions)
    {
        int c = 0;

        //Update existing item OR spawn new
        for (int i = 0; i < actions.Count; i++)
        {
            if (i >= actionItems.Count)
            {
                //New item
                NewActionItem().ShowAndFill(actions[i]);
            }
            else
            {
                //Existing item
                actionItems[i].ShowAndFill(actions[i]);
            }
            c++;
        }

        //On cache les items restant
        for (; c < actionItems.Count; c++)
        {
            actionItems[c].Hide();
        }
    }



    private HBD_Action NewActionItem()
    {
        HBD_Action newItem = Instantiate(actionItemPrefab.gameObject, container).GetComponent<HBD_Action>();
        actionItems.Add(newItem);
        newItem.onDeleteClick = onDeleteActionClick;
        newItem.onDragOut = onDragOut;
        return newItem;
    }
}
