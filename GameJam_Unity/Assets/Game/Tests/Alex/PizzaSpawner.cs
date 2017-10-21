using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    public Pizza pizzaPrefab;

    public float spawnCooldown = 2;

    private int pizzaToSpawn;
    private bool readyToSpawn;
    private Node myNode;
    public float spawningIn = 0;

    void Start()
    {
        myNode = GetComponent<Node>();

        enabled = false;
        Game.OnGameReady += () => enabled = true;
    }

    void Update()
    {
        if (myNode.GetPizza() != null)
            return;

        if (spawningIn >= 0)
        {
            spawningIn -= Time.deltaTime;
            if (spawningIn < 0)
                Spawn();
        }
    }

    void Spawn()
    {
        Pizza newPizza = Instantiate(pizzaPrefab, transform.position, transform.rotation, Game.instance.unitCountainer.transform);

        newPizza.DroppedOn(myNode);

        spawningIn = spawnCooldown;
    }
}
