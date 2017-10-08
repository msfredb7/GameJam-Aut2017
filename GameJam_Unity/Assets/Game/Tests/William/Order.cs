using System;
using CCC.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Tests.William
{
    public class Order : MonoBehaviour
    {
        [SerializeField] private GameObject objectiveWarningObject;
        [SerializeField] private GameObject countdownObject;
        [SerializeField] private GameObject pizzaCountObject;
        private RectTransform rectTransform;

        public Node Node { get; set; }
        public GameObject UICountdown { get; set; }
        public float TimeRemaining { get; set; }
        public int PizzaAmount { get; set; }
        private bool isOrderStarted;
        private ClientManager clientManager;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            transform.localScale = Vector3.one;
        }

        void Update()
        {
            rectTransform.position = Camera.main.WorldToScreenPoint(Node.Position);
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining < 0)
            {
                clientManager.RemoveFromOrderList(gameObject);
                Node.Order = null;
                NotificationQueue.PushNotification("Vous avez manquer une livraison !");
                Destroy(gameObject);
            }
            else if (TimeRemaining <= clientManager.TimeRemainingWarning)
            {
                objectiveWarningObject.GetComponent<Image>().enabled = true;
                countdownObject.GetComponent<Text>().color = Color.white;
            }

            countdownObject.GetComponent<Text>().text = Convert.ToString((int)TimeRemaining);
            pizzaCountObject.GetComponent<Text>().text = Convert.ToString(PizzaAmount - Node.pizza.Count);
        }

        public void SetClientManager(ClientManager clientManager)
        {
            this.clientManager = clientManager;
        }
    }
}
