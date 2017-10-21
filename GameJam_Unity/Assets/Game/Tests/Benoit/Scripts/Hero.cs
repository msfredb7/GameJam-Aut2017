using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    //  Maximum attainable speed
    [ReadOnlyInPlayMode]
    public float maxSpeed = 10;

    //  Speed unit Gained by second
    [ReadOnlyInPlayMode]
    public float acceleration_ = 10;
    
    //  Maximum turning angle in radian
    [ReadOnlyInPlayMode]
    public float maxTuringAngle = Mathf.PI / 4;
    
    //  Max turning Speed
    [ReadOnlyInPlayMode]
    public float turningMaxSpeed = 4;

    [ReadOnlyInPlayMode]
    public HeroDescription heroDescription;

    public Pizza carriedPizza;

    public HeroBehavior behavior;
    public Brain brain;
    public SpriteRenderer faceSpriteRenderer;


    private float currentSpeed;

    [ReadOnly]
    public Node currentNode = null;
    [ReadOnly]
    public Node previousNode = null;
    [ReadOnly]
    public Node nextNode = null;

    private bool moving = false;

    public delegate void HeroEvent(Hero hero);

    public Action onReachNode;
    public event HeroEvent onClick;

    private void Update()
    {
        if (carriedPizza != null)
        {
            carriedPizza.transform.position = transform.position + new Vector3(0.75f, 0.75f, 0);
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
    }

    public void Stop()
    {
        moving = false;
        currentSpeed = 0;
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

            float angle = Vector2.Angle(nextLink, previousLink);

            if (angle > maxTuringAngle)
                currentSpeed = currentSpeed.Capped(turningMaxSpeed);
        }
    }


    void OnMouseDown()
    {
        if (onClick != null)
        {
            if (Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 15)
              return;
            onClick(this);
            
        }
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

        Transform tr = transform;
        Vector3 myPos = tr.position;

        Vector2 destination = nextNode.Position;
        Vector2 delta = destination - (Vector2)myPos;

        if (delta.sqrMagnitude < 0.005)
        {
            transform.position = (Vector3)destination;
            DestinationReached();
            return;
        }
        else
        {
            currentSpeed += acceleration_ * Time.deltaTime;
            currentSpeed = currentSpeed.Capped(maxSpeed);

            transform.position = myPos.MovedTowards(destination, currentSpeed * Time.deltaTime);

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

            brain.currentMode = Brain.Mode.pickup;
            if (closestNode.GetPizza() != null)
                AttemptPizzaCatch(closestNode.GetPizza());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AttemptPizzaCatch(Pizza pizz)
    {
        if (pizz.myHero != null)
            return false;
        
        if (brain.currentMode == Brain.Mode.pickup && carriedPizza == null)
        {
            pizz.PickedUpBy(this);
            return true;
        }
        return false;
    }

    public void Drop(Node onNode)
    {
        if (carriedPizza != null && onNode.pizza.Count <= 0)
        {          
            carriedPizza.DroppedOn(onNode);
        }
    }
}
