using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CCC.Manager;

public class HBD_LooseAction : HBD_Button
{
    public Text displayName;

    public HBD_EmptySpot emptySpotPrefab;

    [ReadOnly]
    public HBD_EmptySpot emptySpotInstance;

    [ReadOnly]
    public HeroActionEvent existingAction;
    [ReadOnly]
    public HBD_ActionList tempList;
    [ReadOnly]
    public HBD_ActionList loopList;
    [ReadOnly]
    public HeroBehavior hb;
    [ReadOnly]
    public HeroBehaviorDisplay display;

    public bool createNew = false;

    public AudioClip sfx_drop;

    public void Fill(HeroBehaviorDisplay display, HeroBehavior hb, HBD_ActionList tempList, HBD_ActionList loopList, HBD_TemplateAction templateAction)
    {
        existingAction = templateAction.actionClone;
        this.loopList = loopList;
        this.tempList = tempList;
        this.display = display;
        this.hb = hb;
        createNew = true;

        SharedFill();
    }
    public void Fill(HeroBehaviorDisplay display, HeroBehavior hb, HBD_ActionList tempList, HBD_ActionList loopList, HBD_Action action)
    {
        existingAction = action.action;
        this.loopList = loopList;
        this.tempList = tempList;
        this.display = display;
        this.hb = hb;
        createNew = false;

        SharedFill();
    }

    void Update()
    {
        transform.position = Input.mousePosition;


        if (tempList.pointerListener.isIn)
        {
            int c = GetIndexInTempList();

            GetEmptySpot().transform.SetParent(tempList.container);
            GetEmptySpot().transform.SetSiblingIndex(c);
        }
        else if (loopList.pointerListener.isIn)
        {
            int c = GetIndexInLoopList();

            GetEmptySpot().transform.SetParent(loopList.container);
            GetEmptySpot().transform.SetSiblingIndex(c);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //Cancel
            Close();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (tempList.pointerListener.isIn)
            {
                int c = GetIndexInTempList();

                HeroActionEvent action = DeliveredEvent();

                hb.AddTemporaryActionAt(action, c);

                if (action.GetHeroActionInfo().RequiresNode())
                    display.BindNodeWithAction(action);

                Close();
            }
            else if (loopList.pointerListener.isIn)
            {
                int c = GetIndexInLoopList();

                HeroActionEvent action = DeliveredEvent();

                hb.AddActionAt(action, c);

                if (action.GetHeroActionInfo().RequiresNode())
                    display.BindNodeWithAction(action);

                Close();
            }
        }
    }

    int GetIndexInTempList()
    {
        Vector2 mousePos = Input.mousePosition;
        int c = 0;
        for (int i = 0; i < tempList.actionItems.Count; i++)
        {
            if (!tempList.actionItems[i].gameObject.activeSelf)
                break;
            if (tempList.actionItems[i].transform.position.y < mousePos.y)
                break;
            c++;
        }
        return c;
    }

    int GetIndexInLoopList()
    {
        Vector2 mousePos = Input.mousePosition;
        int c = 0;
        for (int i = 0; i < loopList.actionItems.Count; i++)
        {
            if (!loopList.actionItems[i].gameObject.activeSelf)
                break;
            if (loopList.actionItems[i].transform.position.y < mousePos.y)
                break;
            c++;
        }
        return c;
    }

    HBD_EmptySpot GetEmptySpot()
    {
        if (emptySpotInstance == null)
            emptySpotInstance = Instantiate(emptySpotPrefab.gameObject, transform).GetComponent<HBD_EmptySpot>();

        return emptySpotInstance;
    }

    HeroActionEvent DeliveredEvent()
    {
        SoundManager.PlayStaticSFX(sfx_drop);
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

        if (emptySpotInstance != null)
            emptySpotInstance.Destroy();
    }

    void SharedFill()
    {
        displayName.text = existingAction.GetHeroActionInfo().GetDisplayName();
        SetColor(existingAction.GetHeroActionInfo().GetNodeColor());
    }

}
