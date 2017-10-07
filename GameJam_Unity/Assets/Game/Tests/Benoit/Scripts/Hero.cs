using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {


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
            transform.position = closestNode.transform.position;
        else
            Destroy(gameObject);
    }
}
