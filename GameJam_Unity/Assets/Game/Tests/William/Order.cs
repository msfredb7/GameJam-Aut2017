using System;
using CCC.Manager;
using UnityEngine;

namespace Assets.Game.Tests.William
{
    public class Order : MonoBehaviour
    {
        private int timeToDeliver;
        private float timeSinceStart;
        private bool isOrderStarted;
        private ClientManager clientManager;

        void Start()
        {
            timeToDeliver = UnityEngine.Random.Range(William_TestScript.MIN_ORDER_TIMER, William_TestScript.MAX_ORDER_TIMER);
            timeSinceStart = 0;
        }

        void Update()
        {
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart > timeToDeliver)
            {
                clientManager.RemoveFromOrderList(gameObject);
                Destroy(gameObject);
            }
        }

        public void SetClientManager(ClientManager clientManager)
        {
            this.clientManager = clientManager;
        }
    }
}
