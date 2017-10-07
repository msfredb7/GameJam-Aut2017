using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    private List<Vector2> regularOrderList;

    [SerializeField] private GameObject SpawnCircleCenter;
    private int spawnCircleRadius = 5;

    [SerializeField] private GameObject OrderPrefab;

    [SerializeField] private int minPositionChange = 5;
    [SerializeField] private int maxPositionChange = 15;

    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 2f;

    private float spawnTime;

    void Start ()
    {
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
	            ChangeSpawnPointPosition();

	        }
	        else
	        {
	            //spawn on one of the regulars point
	            SpawnRandomRegularClient();
	        }
	        spawnTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
            Debug.Log(spawnTime);
	    }
	}

    public void ChangeSpawnPointPosition()
    {
        float posX = UnityEngine.Random.Range(-(William_TestScript.MAP_SIZE_X * 0.5f), William_TestScript.MAP_SIZE_X*0.5f);
        float posY = UnityEngine.Random.Range(-(William_TestScript.MAP_SIZE_Y * 0.5f), William_TestScript.MAP_SIZE_Y * 0.5f);
        SpawnCircleCenter.transform.position = new Vector2(posX, posY);

        Vector2 spawnPoint = (Vector2)SpawnCircleCenter.transform.position + CCC.Math.Vectors.RandomVector2(0, spawnCircleRadius);

        while (!William_TestScript.MAP_BOUNDS.Contains(spawnPoint))
        {
           spawnPoint = (Vector2)SpawnCircleCenter.transform.position + CCC.Math.Vectors.RandomVector2(0, spawnCircleRadius);
        }
        GameObject order = Instantiate(OrderPrefab);
        order.transform.position = spawnPoint;
    }

    public void SpawnRandomRegularClient()
    {
        Vector2 randomRegular = regularOrderList[UnityEngine.Random.Range(0, regularOrderList.Count)];
        
    }
}
