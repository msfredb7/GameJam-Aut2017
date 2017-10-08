using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour {

    public Pizza pizzaPrefab;

    public bool spawnPointOccupied = false;

    public float spawnCooldown = 2;

    private int pizzaToSpawn;
    private bool readyToSpawn;

	void Start ()
    {
        Game.OnGameStart += Init;
    }

    void Init()
    {
        readyToSpawn = true;
    }
	
	void Update ()
    {
        if (readyToSpawn)
        {
            if(!spawnPointOccupied)
                Spawn();
        }
	}

    void Spawn()
    {
        readyToSpawn = false;
        spawnPointOccupied = true;
        Pizza newPizza = Instantiate(pizzaPrefab,transform.position,transform.rotation,Game.instance.unitCountainer.transform);
        newPizza.pizzaPickedUp += delegate () { spawnPointOccupied = false; };
        DelayManager.LocalCallTo(ReadyToSpawn, spawnCooldown, this);
    }

    void ReadyToSpawn()
    {
        readyToSpawn = true;
    }
}
