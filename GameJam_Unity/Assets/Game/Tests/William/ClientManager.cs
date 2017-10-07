using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Game.Tests.William;
using UnityEngine;

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

    private FAStar faStar;

    private float spawnTime;

    void Start ()
    {
        faStar = GetComponent<FAStar>();
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
	    spawnTime -= Time.deltaTime;

	    if (spawnTime < 0)
	    {
	        int isSpawnPoint = UnityEngine.Random.Range(0, 2);
	        if (isSpawnPoint == 1)
	        {
                //spawn in the spawn point range
                SpawnAtRandomClient();
	        }
	        else
	        {
                //spawn on one of the regulars point
                SpawnAtRandomRegularClient();
            }
	        spawnTime = UnityEngine.Random.Range(minSpawnRate, maxSpawnRate);
	    }
	}

    public void RemoveFromOrderList(GameObject gameObject)
    {
        orders.Remove(gameObject);
    }

    public void SpawnAtRandomClient()
    {
        Vector2 spawnPos = faStar.nodes[UnityEngine.Random.Range(0, faStar.nodes.Count)].Position;
        Node spawnNode = faStar.nodes[UnityEngine.Random.Range(0, faStar.nodes.Count)];
        SpawnOrder(spawnNode);
    }

    public void SpawnAtRandomRegularClient()
    {
        Vector2 spawnPos = GetNodeAt(regularOrderList[UnityEngine.Random.Range(0, regularOrderList.Count)]).Position;
        Node SpawnNode = GetNodeAt(regularOrderList[UnityEngine.Random.Range(0, regularOrderList.Count)]);
        SpawnOrder(SpawnNode);
    }

    private Node GetNodeAt(Vector2 pos)
    {
        for (int i = 0; i < faStar.nodes.Count; i++)
        {
            if (faStar.nodes[i].Position == pos)
            {
                return faStar.nodes[i];
            }
        }
        return null;
    }

    private void SpawnOrder(Node currentNode)
    {
        Vector2 nodePos = currentNode.Position;
        if (!isClientAlreadyOrdering(nodePos))
        {
            GameObject orderObject = Instantiate(OrderPrefab);
            orderObject.transform.position = nodePos;

            Order order = orderObject.GetComponent<Order>();
            currentNode.Order = order;
            order.Node = currentNode;

            order.SetClientManager(this);
            order.NbPizza = UnityEngine.Random.Range(0, maxPizzaPerOrder);
            orders.Add(orderObject);
        }
    }

    private bool isClientAlreadyOrdering(Vector2 newOrderPos)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (newOrderPos == (Vector2)orders[i].transform.position)
            {
                return true;
            }
        }
        return false;
    }
}
