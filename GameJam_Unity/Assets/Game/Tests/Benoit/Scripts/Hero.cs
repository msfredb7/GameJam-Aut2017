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


    public CharacterBehavior behavior;
    public Brain brain;


    private float currentSpeed;

    private Node currentNode = null;

    private Node previousNode = null;
    private Node nextNode = null;

    private bool moving = false;

    public delegate void HeroEvent(Hero hero);

    public Action onReachNode;
    public event HeroEvent onClick;

    private void Update()
    {
        if (moving)
            Move();
    }


    public void SetNode(Node node)
    {
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
        if (currentNode == null)
            return;

        previousNode = currentNode;
        currentNode = nextNode;


        Vector2 previousLink = currentNode.Position - previousNode.Position;
        Vector2 nextLink = currentNode.Position - nextNode.Position;

        float dot = Vector2.Dot(previousLink, nextLink);
        float sumMag = previousLink.magnitude * nextLink.magnitude;
        float angle = Mathf.Acos(dot / sumMag);

        //print(angle);

        if (angle > maxTuringAngle)
            currentSpeed = 0;
        else if (currentSpeed > turningSpeed)
            currentSpeed = turningSpeed;
    }


    void OnMouseDown()
    {
        if (onClick != null)
            onClick(this);
    }

    public void DestinationReached()
    {
        if (onReachNode != null)
            onReachNode();
    }


    public void Move()
    {
        if (currentNode == null)
            return;

        Vector2 destination = currentNode.Position;
        Vector2 delta = destination - (Vector2)transform.position;

        if (delta.magnitude < currentSpeed)
        {
            transform.position = (Vector3)destination;
            DestinationReached();
            return;
        }
        else
        {
            transform.position += ((Vector3)delta).normalized * currentSpeed;
            if (currentSpeed < limitSpeed)
            {
                currentSpeed += accelerationRate * Time.deltaTime;
            }
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
            Destroy(gameObject);
    }
}
