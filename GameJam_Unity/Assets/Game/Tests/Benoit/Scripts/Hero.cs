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
        //  Maximum turning angle in radian
    [ReadOnlyInPlayMode]
    public float maxTuringAngle = Mathf.PI / 9;


    private float currentSpeed;

    private List<nodesTest> currentPath = new List<nodesTest>();
    private int pathIterator;

    private nodesTest startingPoint;
    private bool moving = false;




    /*TEST*/


    private void DebugNextNode()
    {
        List<nodesTest> wooo = new List<nodesTest>();
        print(currentPath.Count);
        if (currentPath.Count == 0)
        {
            wooo.Add(startingPoint.nextNode);
        }
        else
            wooo.Add(currentPath[currentPath.Count - 1].nextNode);


        AppendPath(wooo);
    }

    private void DebugPreviousNode()
    {
        List<nodesTest> wooo = new List<nodesTest>();

        if (currentPath.Count == 0)
        { 
            wooo.Add(startingPoint.previousNode);
        }
        else
            wooo.Add(currentPath[currentPath.Count - 1].previousNode);

        AppendPath(wooo);
    }


    private void Update()
    {
        if (startingPoint == null)
            return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DebugPreviousNode();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DebugNextNode();
        }


        /*TEST*/

        if (moving)
            Move();
    }


    public void AppendPath(List<nodesTest> newPath)
    {
        if (currentPath == null)
            currentPath = new List<nodesTest>();

        int size = newPath.Count;
        for (int i = 0; i < size; i++)
        { 
            currentPath.Add(newPath[i]);
        }

        moving = true;
    }

    public void SetToNode(nodesTest node)
    {
        transform.position = node.transform.position;
        startingPoint = node;
    }

    public void ToNextNode()
    {
        
        if (currentPath.Count == 1)
        {
            DestinationReached();
            return;
        }

        currentPath.RemoveAt(0);

        if (0 < pathIterator && pathIterator + 1 < currentPath.Count)
        {
            Vector2 previousLink = currentPath[pathIterator].transform.position - currentPath[pathIterator - 1].transform.position;
            Vector2 nextLink = currentPath[pathIterator].transform.position - currentPath[pathIterator + 1].transform.position;
            float dot = Vector2.Dot(previousLink, nextLink);
            float sumMag = previousLink.magnitude * nextLink.magnitude;
            float angle = Mathf.Acos(dot / sumMag);

            if (angle < maxTuringAngle)
                currentSpeed = 0;
        }
    }

    void OnMouseDown()
    {
        print("tortue de terre");
    }




    public void DestinationReached()
    {
        moving = false;
        startingPoint = currentPath[0];
        currentPath.RemoveAt(0);
        pathIterator = 0;
        currentSpeed = 0;
    }

    public void Move()
    {
        if (currentPath == null)
            return;

        Vector2 destination = (Vector2)currentPath[0].transform.position;
        Vector2 delta = destination - (Vector2)transform.position;

        if (delta.magnitude < currentSpeed)   {
            transform.position = (Vector3)destination;
            ToNextNode();
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
            print("exist");
            SetToNode(closestNode);
        }

        else
            Destroy(gameObject);





    }
}
