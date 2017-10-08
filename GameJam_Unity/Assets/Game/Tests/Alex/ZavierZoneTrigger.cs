using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZavierZoneTrigger : MonoBehaviour {

    public ZavierZone zone;

    public void OnTriggerEnter2D(Collider2D col)
    {
        print("enter");

        Node n = col.GetComponent<Node>();

        if (n != null)
        {
            zone.nodes.Add(n);
            return;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        print("exit");

        Node n = col.GetComponent<Node>();

        if (n != null)
        {
            zone.nodes.Remove(n);
            return;
        }
    }
}
