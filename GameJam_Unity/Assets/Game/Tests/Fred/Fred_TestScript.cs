using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Fred_TestScript : MonoBehaviour
{
    private void Awake()
    {
        print("Creation des lien bidirectionel");
        for (int i = 0; i < transform.childCount; i++)
        {
            Node n = transform.GetChild(i).GetComponent<Node>();
            if (n != null)
            {
                if (n.voisins.Count == 0 && !Application.isPlaying)
                {
                    print("node sans voisin: " + n.name);
                    n.gameObject.SetActive(false);
                    continue;
                }
                n.gameObject.SetActive(true);

                for (int u = 0; u < n.voisins.Count; u++)
                {
                    n.other = n.voisins[u];
                    n.BuildBidirectionalLink();
                }
            }
        }

        if (!Application.isPlaying)
            print("nombre de node: " + transform.childCount);
    }
}
