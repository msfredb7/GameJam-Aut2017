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
            zone.collider.enabled = true;
            zone.zonePreview.DOFade(1, 0.5f);
            myOnComplete = delegate ()
            {
                zone.remoteFixedUpdater = null;
                zone.collider.enabled = false;
                zone.zonePreview.DOFade(0, 0.5f);
                onComplete.Invoke();
            };

            zone.remoteFixedUpdater = RemoteUpdate;
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
            //Not going to pizza anymore
            currentPizzaNode = null;
            goingToPizza = false;

            //Go to order
            if (!goingToOrder)
            {
                  hero.brain.GoToNode(currentOrderNode, Brain.Mode.drop);
                  goingToOrder = true;
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
                hero.brain.GoToNode(currentPizzaNode);
                goingToPizza = true;
            }
        }
    }

    private bool CheckOrder()
    {
        if (currentOrderNode != null && currentOrderNode.Order != null)
            return true;

        Node newOrderNode = zone.GetClosestNodeWithOrder();
        
        goingToOrder = false;

        // Nouvelle order ?
        if (newOrderNode != null)
        {
            currentOrderNode = newOrderNode;
            return true;
        }

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

        goingToPizza = false;

        // Nouvelle order ?
        if (newPizzaNode != null)
        {
            currentPizzaNode = newPizzaNode;
            return true;
        }

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
        goingToPizza = false;
        goingToOrder = false;
    }
}
