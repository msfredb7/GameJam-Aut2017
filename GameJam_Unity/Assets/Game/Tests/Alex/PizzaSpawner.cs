using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour {

    public Pizza pizzaPrefab;

    public bool spawnPointOccupied;

    public float spawnCooldown = 2;

    private int pizzaToSpawn;
    private bool readyToSpawn;

    public void Request()
    {
        pizzaToSpawn++;
        if (readyToSpawn)
            Spawn();
    }

	void Start ()
    {
        readyToSpawn = true;
    }
	
	void Update ()
    {
        if (readyToSpawn)
        {
            Spawn();
        }
	}

    void Spawn()
    {
        readyToSpawn = false;
        Instantiate(pizzaPrefab, Game);
        DelayManager.LocalCallTo(ReadyToSpawn, spawnCooldown, this);
    }

    void ReadyToSpawn()
    {
        readyToSpawn = true;
    }
}
