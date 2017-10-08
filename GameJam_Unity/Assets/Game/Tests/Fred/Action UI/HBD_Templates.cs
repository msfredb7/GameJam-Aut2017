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

    [Header("Dynamic")]
    public List<HBD_TemplateAction> templates;

    public Action<HBD_TemplateAction> onNewActionClick;

    public void Fill(List<HeroActionEvent> clones)
    {
        int c = 0;


        //Update existing items OR spawn new
        for (int i = 0; i < clones.Count; i++)
        {
            if (i >= templates.Count)
            {
                //New item
                NewActionItem().ShowAndFill(clones[i]);
            }
            else
            {
                //Existing item
                templates[i].ShowAndFill(clones[i]);
            }

            //Set parent container
            templates[i].transform.SetParent(clones[i].GetHeroActionInfo().IsUnique() ? specialContainer : stdContainer);

            c++;
        }

        //On cache les items restant
        for (; c < templates.Count; c++)
        {
            templates[c].Hide();
        }
    }

    private HBD_TemplateAction NewActionItem()
    {
        HBD_TemplateAction newItem = Instantiate(templatePrefab.gameObject, stdContainer).GetComponent<HBD_TemplateAction>();
        templates.Add(newItem);
        newItem.onClick = onNewActionClick;
        //newItem.onDragOut = onDragOut;
        return newItem;
    }
}
