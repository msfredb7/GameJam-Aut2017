using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZavierZoneTrigger : MonoBehaviour {

    public ZavierZone zone;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color.ChangedAlpha(0);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Node n = col.GetComponent<Node>();

        if (n != null)
        {
            zone.nodes.Add(n);
            return;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        Node n = col.GetComponent<Node>();

        if (n != null)
        {
            zone.nodes.Remove(n);
            return;
        }
    }
}
