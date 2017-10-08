using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Fred_TestScript : MonoBehaviour
{
    private void Awake()
    {
        print("tolo");
        for (int i = 0; i < transform.childCount; i++)
        {
            Node n = transform.GetChild(i).GetComponent<Node>();
            if (n != null)
            {
                for (int u = 0; u < n.voisins.Count; u++)
                {
                    n.other = n.voisins[u];
                    n.BuildBidirectionalLink();
                }
            }
        }
    }
}
