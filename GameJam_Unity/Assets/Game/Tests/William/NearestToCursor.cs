using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestToCursor : MonoBehaviour
{

    private Vector3 mouseToWorldPos;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private SpriteRenderer nodeMarker;

    // Use this for initialization
    void Start()
    {
        enabled = false;
        Game.OnGameReady += () => enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        mouseToWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseToWorldPos = new Vector3(mouseToWorldPos.x, mouseToWorldPos.y, 10f);

        Node nearestNode = null;

        for (int i = 0; i < Game.Fastar.nodes.Count; i++)
        {
            Node currentNode = Game.Fastar.nodes[i];

            if (nearestNode == null)
            {
                nearestNode = currentNode;
            }

            Vector2 offset = currentNode.Position - (Vector2)mouseToWorldPos;
            Vector2 offset2 = (Vector2)mouseToWorldPos - nearestNode.Position;
            if (offset.sqrMagnitude < offset2.sqrMagnitude)
            {
                nearestNode = currentNode;
            }

            //if (nearestNode == null)
            //{
            //    nearestNode = currentNode;
            //}
            //if (Vector3.Distance(mouseToWorldPos, currentNode.Position) <=
            //    Vector3.Distance(mouseToWorldPos, nearestNode.Position))
            //{
            //    nearestNode = currentNode;
            //}
        }

        nodeMarker.transform.position = nearestNode.Position;
        nodeMarker.enabled = true;

        //Debug.Log("Nearest Node : " + nearestNode.Position);
    }
}
