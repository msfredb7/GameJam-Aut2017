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
            timeToDeliver = UnityEngine.Random.Range(William_TestScript.MIN_ORDER_TIMER, William_TestScript.MAX_ORDER_TIMER);
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
