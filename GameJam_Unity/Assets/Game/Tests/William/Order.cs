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
        private float timeRemaining;
        private bool isOrderStarted;
        private ClientManager clientManager;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            timeRemaining = UnityEngine.Random.Range(William_TestScript.MIN_ORDER_TIMER, William_TestScript.MAX_ORDER_TIMER);
            transform.localScale = Vector3.one;
        }

        void Update()
        {
            rectTransform.position = Camera.main.WorldToScreenPoint(Node.Position);   
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                clientManager.RemoveFromOrderList(gameObject);
                Node.Order = null;
                Destroy(gameObject);
            }
            else if (timeRemaining <= clientManager.TimeRemainingWarning)
            {
                objectiveWarningObject.GetComponent<Image>().enabled = true;
                countdownObject.GetComponent<Text>().color = Color.black;
            }

            countdownObject.GetComponent<Text>().text = Convert.ToString((int)timeRemaining);
            pizzaCountObject.GetComponent<Text>().text = "0";
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
