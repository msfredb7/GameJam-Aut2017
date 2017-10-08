using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBD_LooseAction : HBD_Button
{
    [ReadOnly]
    public HeroActionEvent existingAction;

    public bool createNew = false;

    public void Fill(HBD_TemplateAction templateAction)
    {
        existingAction = templateAction.actionClone;
        createNew = true;
    }
    public void Fill(HBD_Action action)
    {
        existingAction = action.action;
        createNew = false;
    }

}
