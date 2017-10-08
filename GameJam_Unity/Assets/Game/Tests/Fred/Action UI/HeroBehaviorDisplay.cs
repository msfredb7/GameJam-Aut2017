using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBehaviorDisplay : MonoBehaviour
{
    [Header("Loose Action")]
    public HBD_LooseAction looseActionPrefab;

    [Header("Lists")]
    public HBD_Templates templates;
    public HBD_ActionList tempList;
    public HBD_ActionList loopList;

    public delegate void NodeEvent(Node node);
    public delegate void NodeRequest(NodeEvent callback);
    public NodeRequest nodeRequest;

    private HeroBehavior hb;

    void Awake()
    {
        //Templates
        templates.onNewActionClick = OnNewActionClick;

        //Temporary list
        tempList.onDeleteActionClick = OnDeleteFromTempList;
        tempList.onDragOut = OnDragOutFromTempList;

        //Loop list
        loopList.onDeleteActionClick = OnDeleteFromLoopList;
        loopList.onDragOut = OnDragOutFromLoopList;
    }

    public void ClearHB()
    {
        if (hb != null)
        {
            hb.onListChange = null;
            hb.onTemporaryListChange = null;
        }

        hb = null;
    }

    public void Fill(Hero hero)
    {
        ClearHB();

        hb = hero.behavior;
        hb.onListChange = ReFill;
        hb.onTemporaryListChange = ReFill;

        templates.Fill(hb.GetPossibleActionList());

        tempList.Fill(GetTemporaryActions());

        loopList.Fill(GetLoopActions());
    }
    public void ReFill()
    {
        Fill(hb.hero);
    }


    void OnNewActionClick(HBD_TemplateAction template)
    {
        //Instantiate une looseAction, et la remplir de l'information
        print("Nouvelle action de type: " + template.actionClone.GetHeroActionInfo().GetDisplayName());

        HBD_LooseAction looseAction = Instantiate(looseActionPrefab.gameObject, transform.parent).GetComponent<HBD_LooseAction>();
        looseAction.Fill(this, hb, tempList, loopList, template);

        //looseAction.fi
        //if (putInTemp.isOn)
        //    hb.AddTemporaryAction(template.actionClone.Clone());
        //else
        //    hb.AddAction(template.actionClone.Clone());
    }

    void OnDeleteFromTempList(HBD_Action actionUI)
    {
        print("Delete from temp list");
        hb.RemoveActionFromTemporaryList(actionUI.action);
        //...
    }
    void OnDragOutFromTempList(HBD_Action actionUI)
    {
        print("Drag out from temp list");
        //...
    }

    void OnDeleteFromLoopList(HBD_Action actionUI)
    {
        print("Delete from loop list");

        hb.RemoveActionFromLoopList(actionUI.action);
        //...
    }
    void OnDragOutFromLoopList(HBD_Action actionUI)
    {
        print("Drag out from loop list");
        //...
    }

    List<HeroActionEvent> GetTemporaryActions()
    {
        return hb.temporaryCharacterActions;
    }
    List<HeroActionEvent> GetLoopActions()
    {
        return hb.characterActions;
    }

    public void BindNodeWithAction(HeroActionEvent action)
    {
        nodeRequest((Node theNode) =>
        {
            action.GetHeroActionInfo().GiveNode(theNode);
        });
    }
}
