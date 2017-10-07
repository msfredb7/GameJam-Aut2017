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

    public Brain brain;


    private float currentSpeed;

    private List<Node> currentPath = new List<Node>();
    private int pathIterator;

    private Node startingPoint;
    private bool moving = false;




    /*TEST*/


    private void DebugNextNode()
    {
        List<Node> wooo = new List<Node>();
        if (currentPath.Count == 0)
        {
            wooo.Add(GetVoisinsAleatoir(startingPoint));
        }
        else
            wooo.Add(GetVoisinsAleatoir(currentPath[currentPath.Count - 1]));


        AppendPath(wooo);
    }



    private Node GetVoisinsAleatoir(Node n)
    {
        List<Node> voisins = n.voisins;
        int r = Random.Range(0, voisins.Count);
        return voisins[r];
    }

    private void Update()
    {
        if (startingPoint == null)
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


    public void AppendPath(List<Node> newPath)
    {
        if (currentPath == null)
            currentPath = new List<Node>();

        int size = newPath.Count;
        for (int i = 0; i < size; i++)
        { 
            currentPath.Add(newPath[i]);
        }

        moving = true;
    }

    public void SetToNode(Node node)
    {
        transform.position = node.Position;
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
            Vector2 previousLink = currentPath[pathIterator].Position - currentPath[pathIterator - 1].Position;
            Vector2 nextLink = currentPath[pathIterator].Position - currentPath[pathIterator + 1].Position;
            float dot = Vector2.Dot(previousLink, nextLink);
            float sumMag = previousLink.magnitude * nextLink.magnitude;
            float angle = Mathf.Acos(dot / sumMag);

            //print(angle);

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
            if(currentSpeed < limitSpeed)
                currentSpeed += accelerationRate * Time.deltaTime;
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
