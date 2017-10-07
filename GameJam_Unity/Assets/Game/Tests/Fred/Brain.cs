using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public Hero hero;

    public struct State
    {
        public Node.HeroTransition transition;
        public Node stayNode;

        public bool IsInTransition()
        {
            return transition != null;
        }

        public Node GetNextOrStayNode()
        {
            if (!IsInTransition())
                return stayNode;
            return transition.to;
        }
    }

    public enum Mode { drop = 0 , pickup = 1 }

    public Mode currentMode;
    
    public PathOfDoom currentPath;
    public State state;

    public Action onDestinationReached;

    void Awake()
    {
        hero.onReachNode = OnCompleteTransition;
    }

    public void GoToNode(Node destination, Mode mode = Mode.pickup, Action onReached =null)
    {
        onDestinationReached = onReached;
        currentMode = mode;

        //Meme chemin
        if (currentPath != null && destination == currentPath.GetDestination())
            return;

        if (state.IsInTransition())
        {
            if (destination == state.transition.to)
            {
                //On va deja a la bonne place, on arrete la
                ClearPath();
                print("SHIT NIGGA CRACK");
                return;
            }
            else if (destination == state.transition.from)
            {
                //On reviens sur nos pas et on arrete la
                state.transition.Flip();
                hero.SetNode(state.transition.to);
                ClearPath();
                return;
            }
        } else
        {
            if (state.stayNode == destination)
            {
                OnReachDest();
                return;
            }
        }

        currentPath = Game.Fastar.CalculatePath(state.GetNextOrStayNode(), destination);

        if (state.IsInTransition())
        {
            //Shit ! Go back !
            if (currentPath.Get2ndClosest() == state.transition.from)
            {
                OnCompleteTransition();
            }
        }
        else
        {
            PerformNextSegment();
        }
    }

    public void ClearPath()
    {
        currentPath = null;
    }

    public void OnCompleteTransition()
    {
        state.stayNode = state.transition.to;

        //Delete l'ancienne transition si elle existe
        state.transition.NoticeDelete();

        if (currentPath != null)
            PerformNextSegment();
        else
            OnReachDest();
    }

    private void OnReachDest()
    {
        currentPath = null;
        hero.Stop();

        switch (currentMode)
        {
            case Mode.drop:
                hero.Drop();
                break;
            case Mode.pickup:
                break;
            default:
                break;
        }

        if (onDestinationReached != null)
        {
            Action theAction = onDestinationReached;
            onDestinationReached = null;
            theAction();
        }
        //print("On est arrivé, on clear le path et on stop le héro");
    }

    public void PerformNextSegment()
    {
        if (currentPath.nodes.Count <= 1)
        {
            //On est arrivé !!
            OnReachDest();
        }
        else
        {
            //Nouvelle transition
            Node.HeroTransition newTransition = new Node.HeroTransition();

            //From <= la premiere node du chemin
            newTransition.from = currentPath.GetClosest();
            newTransition.theHero = hero;

            //On enleve la node
            currentPath.RemoveClosest();
            //To <= la NOUVELLE premiere node du chemin. On est présentement entrain de s'y rendre
            newTransition.to = currentPath.GetClosest();

            newTransition.NoticeCreate();

            hero.SetNode(newTransition.to);
            //print("Next Node !");
        }
    }
}
