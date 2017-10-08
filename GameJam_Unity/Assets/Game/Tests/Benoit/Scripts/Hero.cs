using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    //  Maximum attainable speed
    [ReadOnlyInPlayMode]
    public float limitSpeed;
    //  Speed unit Gained by second
    [ReadOnlyInPlayMode]
    public float accelerationRate;
    //  Amount of pizza you carry at once
    [ReadOnlyInPlayMode]
    public float carryingCapacity;
    //  Maximum turning angle in radian
    [ReadOnlyInPlayMode]
    public float maxTuringAngle = Mathf.PI / 4;
    //  Max turning Speed
    [ReadOnlyInPlayMode]
    public float turningSpeed = Mathf.PI / 4;
    [ReadOnlyInPlayMode]
    public HeroDescription heroDescription;

    public Pizza carriedPizza;

    public HeroBehavior behavior;
    public Brain brain;
    public SpriteRenderer faceSpriteRenderer;


    private float currentSpeed;

    public Node currentNode = null;
    public Node previousNode = null;
    public Node nextNode = null;

    private bool moving = false;

    public delegate void HeroEvent(Hero hero);

    public Action onReachNode;
    public event HeroEvent onClick;

    private void Update()
    {
        if (carriedPizza != null)
        {
            carriedPizza.transform.position = transform.position - new Vector3(0, 0, -10);
        }

        if (moving)
            Move();
    }

    public void SetNode(Node node)
    {
        previousNode = currentNode;
        currentNode = nextNode;
        nextNode = node;
        SetTurningSpeed();
        moving = true;

        //if (carriedPizza != null && currentNode.Order != null)
        //{
        //    Drop();
        //}
    }

    public void Stop()
    {
        moving = false;
        currentSpeed = 0;

        //if (carriedPizza != null && currentNode.Order != null)
        //{
        //    Drop();
        //}
    }


    public void SetToNode(Node node)
    {
        transform.position = node.Position;
        currentNode = node;
    }

    public void SetTurningSpeed()
    {
        if (currentNode != null && previousNode != null)
        {

            Vector2 previousLink = currentNode.Position - previousNode.Position;
            Vector2 nextLink = nextNode.Position - currentNode.Position;

            float dot = Vector2.Dot(previousLink, nextLink);
            float sumMag = previousLink.magnitude * nextLink.magnitude;
            float angle; //= Mathf.Acos(dot / sumMag);

            angle = Vector2.Angle(nextLink, previousLink);

            if (angle > maxTuringAngle)
                currentSpeed = 0;
            else if (currentSpeed > turningSpeed)
                currentSpeed = turningSpeed;
        }
    }


    void OnMouseDown()
    {
        if (onClick != null)
            onClick(this);
    }

    public void DestinationReached()
    {
        if (carriedPizza != null && nextNode.Order != null)
        {
            Drop(nextNode);
        }
        if (onReachNode != null)
            onReachNode();
    }


    public void Move()
    {
        if (nextNode == null)
            return;

        Vector2 destination = nextNode.Position;
        Vector2 delta = destination - (Vector2)transform.position;

        if (delta.magnitude < currentSpeed)
        {
            transform.position = (Vector3)destination;
            DestinationReached();
            return;
        }
        else
        {
            currentSpeed += accelerationRate * Time.deltaTime;
            currentSpeed = currentSpeed.Capped(limitSpeed);

            transform.position += ((Vector3)delta).normalized * currentSpeed;

            return;
        }
    }

    // Use this for initialization
    public void SnapToNode()
    {

        Node closestNode = Game.Fastar.GetClosestNode((Vector2)transform.position);

        if (closestNode != null)
        {
            SetToNode(closestNode);
            brain.state.stayNode = closestNode;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void AttemptPizzaCatch(Pizza pizz)
    {
        if (pizz.myHero != null)
            return;

        print("Attempt pickup");
        if (brain.currentMode == Brain.Mode.pickup && carriedPizza == null)
        {
            pizz.PickedUpBy(this);
        }
    }

    public void Drop(Node onNode)
    {
        if (carriedPizza != null && onNode.pizza.Count <= 0)
        {
            Debug.Log("DROPING PIZZA");            
            carriedPizza.DroppedOn(onNode);
        }
    }
}
