using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Game.Tests.William;
using CCC.Manager;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{

    private List<GameObject> orders;

    private int spawnCircleRadius = 5;

    [SerializeField] private GameObject OrderPrefab;
    
    [SerializeField] private int minPositionChange = 5;
    [SerializeField] private int maxPositionChange = 15;


    public int TimeRemainingWarning = 4;

    void Start ()
    {
        enabled = false;
        Game.OnGameReady += delegate
        {
            enabled = true;
        };
        orders = new List<GameObject>();
    }

	void Update ()
	{
	    GameObject orderObjectToDelete = null;
        for (int i = 0; i < orders.Count; i++)
        {
            Order currentOrder = orders[i].GetComponent<Order>();
            if (currentOrder != null)
            {
                if(currentOrder.PizzaAmount <= 0)
                {
                    // Commande reussit !
                    orderObjectToDelete = currentOrder.gameObject;
                    RemoveFromOrderList(currentOrder.gameObject);
                    CommandCompleted(currentOrder.Node);
                }
            }
        }
	    if (orderObjectToDelete != null)
	    {
	        Destroy(orderObjectToDelete);
        }
	}

    public void RemoveFromOrderList(GameObject gameObject)
    {
        orders.Remove(gameObject);
    }

    public void SpawnAtRandomClient(ScriptedOrder scriptedOrder)
    {
        Node spawnNode = Game.Fastar.nodes[UnityEngine.Random.Range(0, Game.Fastar.nodes.Count)];
       
        if (!isClientAlreadyOrdering(spawnNode.Position))
        {
            GameObject orderObject = Instantiate(OrderPrefab);
            orderObject.transform.SetParent(Game.GameUI.transform);
            orderObject.transform.SetAsFirstSibling();
            orderObject.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(spawnNode.Position);
            Order order = orderObject.GetComponent<Order>();
            spawnNode.Order = order;
            order.Node = spawnNode;

            order.SetClientManager(this);
            order.PizzaAmount = scriptedOrder.PizzaAmount;
            order.TimeRemaining = scriptedOrder.OrderDuration;

            orders.Add(orderObject);

            Game.GameUI.DeliverNotificationObject.Notify();
        }
    }

    public void SpawnOrder(ScriptedOrder scriptedOrder)
    {
        Order orderItem = SpawnOrder(scriptedOrder.Node);
        orderItem.TimeRemaining = scriptedOrder.OrderDuration;
        orderItem.PizzaAmount = scriptedOrder.PizzaAmount;
    }

    private Node GetNodeAt(Vector2 pos)
    {
        for (int i = 0; i < Game.Fastar.nodes.Count; i++)
        {
            if (Game.Fastar.nodes[i].Position == pos)
            {
                return Game.Fastar.nodes[i];
            }
        }
        return null;
    }

    private Order SpawnOrder(Node currentNode)
    {
        Vector2 nodePos = currentNode.Position;
        if (!isClientAlreadyOrdering(nodePos))
        {
            GameObject orderObject = Instantiate(OrderPrefab);
            orderObject.transform.SetParent(Game.GameUI.transform);
            orderObject.transform.SetAsFirstSibling();
            orderObject.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(nodePos);

            Order order = orderObject.GetComponent<Order>();
            currentNode.Order = order;
            order.Node = currentNode;

            order.SetClientManager(this);

            orders.Add(orderObject);

            Game.GameUI.DeliverNotificationObject.Notify();

            Debug.Log("Spawned at Node : " + order.Node + ". | World to screen Position : " + Camera.main.ScreenToWorldPoint(order.transform.position));
            return order;
        }
        return null;
    }

    private bool isClientAlreadyOrdering(Vector2 newOrderPos)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (newOrderPos == (Vector2)orders[i].GetComponent<Order>().Node.Position)
            {
                return true;
            }
        }
        return false;
    }

    void CommandCompleted(Node node)
    {
        NotificationQueue.PushNotification("Vous avez complété une commande !");

        Objectives currentObjectives = Game.Map.cash;
        int income = currentObjectives.OrderBasePrice + (currentObjectives.PricePerPizza * node.pizza.Count);
        currentObjectives.IncomeCash(income);
        

        for (int i = 0; i < node.pizza.Count; i++)
        {
            Destroy(node.pizza[i].gameObject);
        }
    }
}
