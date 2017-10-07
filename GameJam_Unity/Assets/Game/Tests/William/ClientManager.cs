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

    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 2f;

    private FAStar faStar;

    private float spawnTime;

    void Start ()
    {
        faStar = GetComponent<FAStar>();
        orders = new List<GameObject>();
        spawnTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
        regularOrderList = new List<Vector2>
        {
            new Vector2(0, 0),
            new Vector2(1, 1),
            new Vector2(2, 2),
            new Vector2(3, 5),
            new Vector2(4, 2)
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
	            SpawnRandomClient();
	        }
	        else
	        {
	            //spawn on one of the regulars point
	            //SpawnRandomRegularClient();
	        }
	        spawnTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
            Debug.Log(spawnTime);
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

    public void RemoveFromOrderList(GameObject gameObject)
    {
        orders.Remove(gameObject);
    }

    public void SpawnRandomClient()
    {
        Vector2 spawnPos = faStar.nodes[UnityEngine.Random.Range(0, faStar.nodes.Count)].Position;
        if (!isClientAlreadyOrdering(spawnPos))
        {
            GameObject order = Instantiate(OrderPrefab);
            order.transform.position = spawnPos;
            order.GetComponent<Order>().SetClientManager(this);
            orders.Add(order);
        }
    }

    public void SpawnRandomRegularClient()
    {
        Vector2 randomRegular = regularOrderList[UnityEngine.Random.Range(0, regularOrderList.Count)];
        
    }
}
