using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvents : MonoBehaviour
{
    public float timescale = 1;

    float timer = 0;
    public float GameTime { get { return timer; } }

    private class DelayedAction
    {
        public float at;
        public Action action;
    }

    LinkedList<DelayedAction> delayedActions = new LinkedList<DelayedAction>();

    private void Awake()
    {
        enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime * timescale;
        CheckActionList();
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    void CheckActionList()
    {
        while (delayedActions.First != null)
        {
            LinkedListNode<DelayedAction> node = delayedActions.First;

            if (node.Value.at <= timer)
            {
                node.Value.action();
                delayedActions.Remove(node.Value);
            }
            else
                break;
        }
    }

    public void AddDelayedAction(Action action, float delay)
    {
        DelayedAction da = new DelayedAction() { at = delay + timer, action = action };
        LinkedListNode<DelayedAction> node = delayedActions.First;

        if (node == null)
        {
            delayedActions.AddFirst(da);
        }
        else
            while (true)
            {
                if (node.Value.at > da.at)
                {
                    delayedActions.AddBefore(node, da);
                    break;
                }
                if (node.Next == null)
                {
                    delayedActions.AddAfter(node, da);
                    break;
                }
                node = node.Next;
            }
    }
}
