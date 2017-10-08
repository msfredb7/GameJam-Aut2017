using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZavierZone : MonoBehaviour
{

    private List<Pizza> pizzas = new List<Pizza>();
    public List<Node> nodes = new List<Node>();
    private Node waitingNode;
    public bool activateDeploy;
    public bool isMoving;
    public Action onZoneClear;
    public SpriteRenderer zonePreview;

    void Update()
    {
        if (activateDeploy)
        {
            if (waitingNode == null)
                waitingNode = GetComponent<Hero>().currentNode;
            //Si on est pas en train de d'aller faire une action, on prend une décision
            if (!isMoving)
            {
                if (GetComponent<Hero>().carriedPizza != null)
                {
                    isMoving = true;
                    SearchForNextOrder();
                }
                else
                {
                    SearchForNextPizza();
                }
            }
        }
        
    }

    private void SearchForNextOrder()
    {
        Node closestOrder = GetClosestNodeWithOrder();
        if (closestOrder == null)
        {
            GoWait();
        }
        else
        {
            isMoving = true;
            GetComponent<Hero>().brain.GoToNode(closestOrder, Brain.Mode.drop, delegate
            {
                isMoving = false;
            });
        }
    }

    private void SearchForNextPizza()
    {
        Node closestNodeWithPizza = GetClosestNodeWithPizza();
        if (closestNodeWithPizza == null)
        {
            GoWait();
        }
        else
        {
            isMoving = true;
            GetComponent<Hero>().brain.GoToNode(closestNodeWithPizza, Brain.Mode.drop, delegate
            {
                isMoving = false;
            } );
        }
    }

    private void GoWait()
    {
        isMoving = true;
        GetComponent<Hero>().brain.GoToNode(waitingNode, Brain.Mode.drop, delegate
        {
            if (GetClosestNodeWithOrder() == null && GetClosestNodeWithPizza() == null)
            {
                onZoneClear.Invoke();
                activateDeploy = false;
                waitingNode = null;
            }
            else
            {
                isMoving = false;
            }
        });
    }

    private Node GetClosestNodeWithOrder()
    {
        Node closestNode = null;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Order != null)
            {
                if (closestNode == null)
                {
                    closestNode = nodes[i];
                }
                else
                {
                    if (Vector3.Distance(nodes[i].Position, transform.position) <
                        Vector3.Distance(closestNode.Position, transform.position))
                    {
                        closestNode = nodes[i];
                    }
                }
            }
        }
        return closestNode;
    }

    private Node GetClosestNodeWithPizza()
    {
        Node closestNode = null;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].pizza.Count > 0)
            {
                if (closestNode == null)
                {
                    closestNode = nodes[i];
                }
                else
                {
                    if (Vector3.Distance(nodes[i].Position, transform.position) <
                        Vector3.Distance(closestNode.Position, transform.position))
                    {
                        closestNode = nodes[i];
                    }
                }
            }
        }
        return closestNode;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        print("enter");
       
        Node n = col.GetComponent<Node>();

        if (n != null)
        {
            nodes.Add(n);
            return;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        print("exit");

        Node n = col.GetComponent<Node>();

        if (n != null)
        {
            nodes.Remove(n);
            return;
        }
    }
}
