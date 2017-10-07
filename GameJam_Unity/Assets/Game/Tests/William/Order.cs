using System;
using CCC.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Tests.William
{
    public class Order : MonoBehaviour
    {
        [SerializeField] private GameObject objectiveWarning;

        public Node Node { get; set; }
        public GameObject UICountdown { get; set; }
        public OrderUI OrderUI { get; set; }
        private float timeRemaining;
        private bool isOrderStarted;
        private ClientManager clientManager;

        void Start()
        {
            timeRemaining = UnityEngine.Random.Range(William_TestScript.MIN_ORDER_TIMER, William_TestScript.MAX_ORDER_TIMER);
        }

        void Update()
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                clientManager.RemoveFromOrderList(gameObject);
                Node.Order = null;
                Destroy(OrderUI.gameObject);
                Destroy(gameObject);
            }
            else if (timeRemaining <= clientManager.TimeRemainingWarning)
            {
                objectiveWarning.GetComponent<SpriteRenderer>().enabled = true;
            }

            OrderUI.CountDownObject.GetComponent<Text>().text = Convert.ToString((int)timeRemaining);
            OrderUI.PizzaCountObject.GetComponent<Text>().text = "0";
        }

        public void SetClientManager(ClientManager clientManager)
        {
            this.clientManager = clientManager;
        }

        public int GetTimeRemaining()
        {
            return (int)timeRemaining;
        }
    }
}
