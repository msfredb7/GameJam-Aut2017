using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Game.Tests.William;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{

    private List<GameObject> orders;
    private List<Vector2> regularOrderList;

    [SerializeField] private GameObject SpawnCircleCenter;

    private int spawnCircleRadius = 5;

    [SerializeField] private GameObject OrderPrefab;
    
    [SerializeField] private int minPositionChange = 5;
    [SerializeField] private int maxPositionChange = 15;

    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float maxSpawnRate = 5f;

    [SerializeField] private int maxPizzaPerOrder = 6;

    public int TimeRemainingWarning = 4;

    private float spawnTime;

    void Start ()
    {
        enabled = false;
        Game.OnGameReady += delegate
        {
            enabled = true;
        };
        orders = new List<GameObject>();
        spawnTime = UnityEngine.Random.Range(minSpawnRate, maxSpawnRate);
        regularOrderList = new List<Vector2>
        {
            new Vector2(0.99f, 3.98f),
            new Vector2(-4.95f, -0.06f),
        };
    }

	void Update ()
	{
	    //spawnTime -= Time.deltaTime;

	    //if (spawnTime < 0)
	    //{
	    //    int isSpawnPoint = UnityEngine.Random.Range(0, 2);
	    //    if (isSpawnPoint == 1)
	    //    {
     //           //spawn in the spawn point range
     //           SpawnAtRandomClient();
	    //    }
	    //    else
	    //    {
     //           //spawn on one of the regulars point
     //           SpawnAtRandomRegularClient();
     //       }
	    //    spawnTime = UnityEngine.Random.Range(minSpawnRate, maxSpawnRate);
	    //}
	}

    public void RemoveFromOrderList(GameObject gameObject)
    {
        orders.Remove(gameObject);
    }

    public void SpawnAtRandomClient()
    {
        Node spawnNode = Game.Fastar.nodes[UnityEngine.Random.Range(0, Game.Fastar.nodes.Count)];
        SpawnOrder(spawnNode);
    }

    public void SpawnAtRandomRegularClient()
    {
        Vector2 spawnPos = GetNodeAt(regularOrderList[UnityEngine.Random.Range(0, regularOrderList.Count)]).Position;
        Node SpawnNode = GetNodeAt(regularOrderList[UnityEngine.Random.Range(0, regularOrderList.Count)]);
        SpawnOrder(SpawnNode);
    }

    public void SpawnOrder(ScriptedOrder order)
    {
        SpawnOrder(order.Node).TimeRemaining = order.OrderDuration;
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
}
