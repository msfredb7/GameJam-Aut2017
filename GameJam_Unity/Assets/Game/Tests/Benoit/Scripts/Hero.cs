using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

        //  Maximum attainable speed
    [ReadOnlyInPlayMode]
    public float limitSpeed;
        //  Speed unit Gained by second
    [ReadOnlyInPlayMode]
    public float accelerationRate;
        //  Amount of pizza you carry at once
    [ReadOnlyInPlayMode]
    public float carryingCapacity;
        
    private float currentSpeed;




    /*TEST*/
    private nodesTest currentNode;
    private nodesTest nextNode;
    public void SetNode(nodesTest node)
    {
        currentNode = node;
        moving = false;
    }
    private bool moving = true;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("WWWWWWWWWWW");
            MoveTo(currentNode.previousNode);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTo(currentNode.nextNode);
        }


        /*TEST*/

        if (nextNode != null)
            MoveTo(nextNode);
    }


    public void DestinationReached()
    {
        currentSpeed = 0;
        currentNode = nextNode;
        nextNode = null;
    }

    public void MoveTo(nodesTest nextNo)
    {
        nextNode = nextNo;

        Vector2 destination = (Vector2)nextNode.transform.position;

        Vector2 delta = destination - (Vector2)transform.position;

        if (delta.magnitude < currentSpeed)   {
            transform.position = (Vector3)destination;
            DestinationReached();
            return;
        }
        else { 
            transform.position += ((Vector3)delta).normalized * currentSpeed;
            currentSpeed += accelerationRate * Time.deltaTime;
            return;
        }
    }




    // Use this for initialization
    public void SnapToNode()
    {
        print("asd");
        float minSqrDistance = float.PositiveInfinity;
        nodesTest closestNode = null;

        nodesTest[] nodes = FindObjectsOfType(typeof(nodesTest)) as nodesTest[];
        foreach (nodesTest node in nodes)
        {
            float sqrDistance = (node.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                closestNode = node;
            }


        }
        if (closestNode != null)
        {
            transform.position = closestNode.transform.position;


            /*TEST*/

            currentNode = closestNode;

            /*TEST*/


        }

        else
            Destroy(gameObject);





    }
}
