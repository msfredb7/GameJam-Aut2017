using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HBD_LooseAction : HBD_Button
{
    public Text displayName;

    [ReadOnly]
    public HeroActionEvent existingAction;
    [ReadOnly]
    public HBD_ActionList tempList;
    [ReadOnly]
    public HBD_ActionList loopList;
    [ReadOnly]
    public HeroBehavior hb;

    public bool createNew = false;

    public void Fill(HeroBehavior hb, HBD_ActionList tempList, HBD_ActionList loopList, HBD_TemplateAction templateAction)
    {
        existingAction = templateAction.actionClone;
        this.loopList = loopList;
        this.tempList = tempList;
        this.hb = hb;
        createNew = true;

        SharedFill();
    }
    public void Fill(HeroBehavior hb, HBD_ActionList tempList, HBD_ActionList loopList, HBD_Action action)
    {
        existingAction = action.action;
        this.loopList = loopList;
        this.tempList = tempList;
        this.hb = hb;
        createNew = false;

        SharedFill();
    }

    void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(1))
        {
            //Cancel
            Close();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (tempList.pointerListener.isIn)
            {
                hb.AddTemporaryAction(DeliveredEvent());
                Close();
                print("temp");
            }
            else if (loopList.pointerListener.isIn)
            {
                print("loop");
                hb.AddAction(DeliveredEvent());
                Close();
            }
        }
    }

    HeroActionEvent DeliveredEvent()
    {
        if (createNew)
        {
            return existingAction.Clone();
        }
        else
        {
            return existingAction;
        }
    }

    void Close()
    {
        Destroy(gameObject);
    }

    void SharedFill()
    {
        displayName.text = existingAction.GetHeroActionInfo().GetDisplayName();
        SetColor(existingAction.GetHeroActionInfo().GetNodeColor());
    }

}
