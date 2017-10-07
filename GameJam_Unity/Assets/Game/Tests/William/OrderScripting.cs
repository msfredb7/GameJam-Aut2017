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
        [HideInInspector]
        public bool IsActive;
        public int PizzaAmount;
    }

    [RequireComponent(typeof(ClientManager))]
    public class OrderScripting : MonoBehaviour
    {
        public List<ScriptedOrder> ScriptedOrders;

        private float timeSinceGameStart;
        private ClientManager clientManager;

        void Awake()
        {
            enabled = false;
            global::Game.OnGameReady += delegate
            {
                enabled = true;
            };
        }

        // Use this for initialization
        void Start ()
        {
            timeSinceGameStart = 0;
            clientManager = GetComponent<ClientManager>();

            for (int i = 0; i < ScriptedOrders.Count; i++)
            {
                int index = i;
                global::Game.DelayedEvents.AddDelayedAction(delegate()
                {
                    clientManager.SpawnOrder(ScriptedOrders[index]);
                }, ScriptedOrders[index].TimeToStart);
            }
        }
	
        // Update is called once per frame
        void Update ()
        {
        }
    }
}