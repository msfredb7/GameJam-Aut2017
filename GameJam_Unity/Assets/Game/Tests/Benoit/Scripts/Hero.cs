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
    public float maxTuringAngle = Mathf.PI / 4;
        //  Max turning Speed
    [ReadOnlyInPlayMode]
    public float turningSpeed = Mathf.PI / 4;


    private float currentSpeed;

    private List<Node> currentPath = new List<Node>();


    private Node previousNode = null;
    private Node nextNode = null;

    private bool moving = false;




    /*TEST*/


    private void DebugNextNode()
    {
        Node wooo;
        if (currentPath.Count == 0)
        {
            wooo = GetVoisinsAleatoir(previousNode);
        }
        else
            wooo = GetVoisinsAleatoir(currentPath[currentPath.Count - 1]);


        AppendNode(wooo);
    }



    private Node GetVoisinsAleatoir(Node n)
    {
        List<Node> voisins = n.voisins;
        int r = Random.Range(0, voisins.Count);
        return voisins[r];
    }

    private void Update()
    {
        if (previousNode == null)
            return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DebugNextNode();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DebugNextNode();
        }


        /*TEST*/

        if (moving)
            Move();
    }


    public void AppendNode(Node node)
    {
        if (currentPath == null)
            currentPath = new List<Node>();

        currentPath.Add(node);
        moving = true;
    }


    public void SetToNode(Node node)
    {
        transform.position = node.Position;
        previousNode = node;
    }


    public void ToNextNode(Node destination)
    {
        previousNode = currentPath[0];
        currentPath.RemoveAt(0);

        if (currentPath.Count == 1)
        {
            DestinationReached();
            return;
        }

        nextNode = currentPath[0];

        if (currentPath.Count > 1)
        {

            Vector2 previousLink = currentPath[0].Position - previousNode.Position;
            Vector2 nextLink = currentPath[0].Position - currentPath[1].Position;

            float dot = Vector2.Dot(previousLink, nextLink);
            float sumMag = previousLink.magnitude * nextLink.magnitude;
            float angle = Mathf.Acos(dot / sumMag);

            print(angle);

            if (angle > maxTuringAngle)
                currentSpeed = 0;
            else if (currentSpeed > turningSpeed)
                currentSpeed = turningSpeed;

        }
    }

    void OnMouseDown()
    {
        print("tortue de terre");
    }




    public void DestinationReached()
    {
        moving = false;
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

            ToNextNode(nextNode);
            return;
        }
        else { 
            transform.position += ((Vector3)delta).normalized * currentSpeed;
            if(currentSpeed < limitSpeed)
            { 
                currentSpeed += accelerationRate * Time.deltaTime;
            }
            return;
        }
    }




    // Use this for initialization
    public void SnapToNode()
    {
        float minSqrDistance = float.PositiveInfinity;
        Node closestNode = null;

        Node[] nodes = FindObjectsOfType(typeof(Node)) as Node[];
        foreach (Node node in nodes)
        {
            float sqrDistance = (node.Position - (Vector2)transform.position).sqrMagnitude;
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
