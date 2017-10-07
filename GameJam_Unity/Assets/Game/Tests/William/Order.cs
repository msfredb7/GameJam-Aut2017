using System;
using CCC.Manager;
using UnityEngine;

namespace Assets.Game.Tests.William
{
    public class Order : MonoBehaviour
    {
        private float timeRemaining;
        private bool isOrderStarted;
        private ClientManager clientManager;

        void Start()
        {
            timeRemaining = UnityEngine.Random.Range(William_TestScript.MIN_ORDER_TIMER, William_TestScript.MAX_ORDER_TIMER); ;
        }

        void Update()
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                clientManager.RemoveFromOrderList(gameObject);
                Destroy(gameObject);
            }
        }

        public void SetClientManager(ClientManager clientManager)
        {
            this.clientManager = clientManager;
        }

        public float GetTimeRemaining()
        {
            return timeRemaining;
        }
    }
}
