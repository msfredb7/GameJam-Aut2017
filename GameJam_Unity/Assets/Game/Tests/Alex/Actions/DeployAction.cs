using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeployAction : HeroActionEvent
{
    DeployActionInfo deployActionInfo;
    private Action myOnComplete;
    private ZavierZone zone;
    private Hero hero;

    private Node currentOrderNode;
    private Node currentPizzaNode;
    private bool goingToPizza;
    private bool goingToOrder;

    public DeployAction()
    {
        deployActionInfo = new DeployActionInfo();
    }

    public override Action OnComplete
    {
        get
        {
            return myOnComplete;
        }
        set
        {
            myOnComplete = value;
        }
    }

    public override void Execute(Hero hero, Action onComplete)
    {
        // deploy
        this.hero = hero;
        zone = hero.GetComponent<ZavierZone>();

        if (zone != null)
        {
            zone.zonePreview.DOFade(1, 0.5f);
            myOnComplete = delegate ()
            {
                zone.zonePreview.DOFade(0, 0.5f);
                onComplete.Invoke();
            };

            zone.remoteUpdater = RemoteUpdate;
        }
        else
        {
            Debug.LogError("t un moron");
            myOnComplete = onComplete;
            ForceCompletion();
        }
    }

    private void RemoteUpdate()
    {
        if (!CheckOrder())
        {
            //No order ! Quit
            ForceCompletion();
            return;
        }

        if (CheckCarriedPizza())
        {
            //Go to order

            //Go to pizza
            if (!goingToOrder)
            {
                hero.brain.GoToNode(currentOrderNode, Brain.Mode.pickup);
            }
        }
        else
        {
            //Check available pizzas
            if (!CheckPizzaNode())
            {
                //No pizza ! Quit
                ForceCompletion();
                return;
            }

            //Go to pizza
            if (!goingToPizza)
            {
                hero.brain.GoToNode(currentPizzaNode, Brain.Mode.pickup);
            }
        }
    }

    private bool CheckOrder()
    {
        if (currentOrderNode != null && currentOrderNode.Order != null)
            return true;

        Node newOrderNode = zone.GetClosestNodeWithOrder();
        
        // Nouvelle order ?
        if(newOrderNode != null)
        {
            currentOrderNode = newOrderNode;
            return true;
        }

        goingToOrder = false;

        return false;
    }
    private bool CheckCarriedPizza()
    {
        return hero.carriedPizza != null;
    }
    private bool CheckPizzaNode()
    {
        if (currentPizzaNode != null && currentPizzaNode.pizza != null)
            return true;

        Node newPizzaNode = zone.GetClosestNodeWithPizza();

        // Nouvelle order ?
        if (newPizzaNode != null)
        {
            currentPizzaNode = newPizzaNode;
            return true;
        }

        goingToPizza = false;

        return false;
    }

    public override HeroActions GetHeroActionInfo()
    {
        return deployActionInfo;
    }

    public override void PostCloneCleanup()
    {
        deployActionInfo = new DeployActionInfo();
        zone = null;
        myOnComplete = null;
        hero = null;
        currentOrderNode = null;
        currentPizzaNode = null;
    }
}
