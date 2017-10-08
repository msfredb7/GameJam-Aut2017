using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HBD_TemplateAction : HBD_Button
{
    public Text displayText;

    public Action<HBD_TemplateAction> onClick;

    public HeroActionEvent actionClone;

    public void ShowAndFill(HeroActionEvent clone)
    {
        actionClone = clone;

        gameObject.SetActive(true);
        displayText.text = clone.GetHeroActionInfo().GetDisplayName();
        SetColor(clone.GetHeroActionInfo().GetNodeColor());
    }

    public void Click()
    {
        if (onClick != null)
            onClick(this);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
