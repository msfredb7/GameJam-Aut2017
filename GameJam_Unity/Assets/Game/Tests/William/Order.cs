using System;
using CCC.Manager;
using UnityEngine;

namespace Assets.Game.Tests.William
{
    public class Order : MonoBehaviour
    {
        public Vector3 DropPoint { get; set; }
        private int timeToDeliver;
        private float timeSinceStart;
        private bool isOrderStarted;

        void Start()
        {
            DropPoint = transform.position;
            timeToDeliver = UnityEngine.Random.Range(OrderManager.MIN_ORDER_TIMER, OrderManager.MAX_ORDER_TIMER);
            timeSinceStart = 0;
        }

        void Update()
        {
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart > timeToDeliver)
            {
                TimesUp();
            }
        }

        private void TimesUp()
        {
            Debug.Log("Order canceled. You goofed too long");
            Destroy(gameObject);
        }
    }
}
