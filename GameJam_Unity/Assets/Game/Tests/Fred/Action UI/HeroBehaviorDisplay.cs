using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehaviorDisplay : MonoBehaviour
{
    [Header("Loose Action")]
    public HBD_LooseAction looseActionPrefab;

    [Header("Lists")]
    public HBD_Templates templates;
    public HBD_ActionList tempList;
    public HBD_ActionList loopList;

    void Start()
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

    public void Fill()
    {
        templates.Fill(/*...*/);
        tempList.Fill(GetTemporaryActions());
        loopList.Fill(GetLoopActions());
    }


    void OnNewActionClick(HBD_TemplateAction template)
    {
        //Instantiate une looseAction, et la remplir de l'information
    }

    void OnDeleteFromTempList(HBD_Action actionUI)
    {
        print("Delete from temp list");
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
        //...
    }
    void OnDragOutFromLoopList(HBD_Action actionUI)
    {
        print("Drag out from loop list");
        //...
    }

    List<HeroActions> GetTemporaryActions()
    {
        List<HeroActions> testList = new List<HeroActions>();
        for (int i = 0; i < 3; i++)
        {
            testList.Add(null);
        }
        return testList;
    }
    List<HeroActions> GetLoopActions()
    {
        List<HeroActions> testList = new List<HeroActions>();
        for (int i = 0; i < 2; i++)
        {
            testList.Add(null);
        }
        return testList;
    }
}
