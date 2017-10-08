using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Tests.William
{
    [System.Serializable]
    public struct ScriptedOrder
    {
        public Node Node;
        public float TimeToStart;
        public float OrderDuration;
        public int PizzaAmount;
    }

    [RequireComponent(typeof(ClientManager))]
    public class OrderScripting : MonoBehaviour
    {
        [SerializeField] private float minSpawnRate = 1f;
        [SerializeField] private float maxSpawnRate = 30f;
        [SerializeField] private float minOrderDuration = 20f;
        [SerializeField] private float maxOrderDuration = 60f;
        [SerializeField] private int maxPizzaPerOrder = 3;

        public List<ScriptedOrder> ScriptedOrders;

        private ClientManager clientManager;

        private float timeToNextSpawn;

        void Awake()
        {
            enabled = false;
            global::Game.OnGameReady += delegate
            {
                enabled = true;
            };
        }

        void Start ()
        {
            clientManager = GetComponent<ClientManager>();

            for (int i = 0; i < ScriptedOrders.Count; i++)
            {
                int index = i;
                global::Game.DelayedEvents.AddDelayedAction(delegate()
                {
                    clientManager.SpawnOrder(ScriptedOrders[index]);
                }, ScriptedOrders[index].TimeToStart);
            }
            timeToNextSpawn = UnityEngine.Random.Range(minSpawnRate, maxSpawnRate);
        }
	
        // Update is called once per frame
        void Update ()
        {
            timeToNextSpawn -= Time.deltaTime;
            if (timeToNextSpawn < 0)
            {
                SpawnRandomScriptedOrder();
                timeToNextSpawn = UnityEngine.Random.Range(minSpawnRate, maxSpawnRate);
            }
        }

        private void SpawnRandomScriptedOrder()
        {
            ScriptedOrder scriptedOrder = new ScriptedOrder();
            scriptedOrder.OrderDuration = UnityEngine.Random.Range(minOrderDuration, maxOrderDuration);
            scriptedOrder.PizzaAmount = UnityEngine.Random.Range(0, maxPizzaPerOrder);
            clientManager.SpawnAtRandomClient(scriptedOrder);
        }
    }
}