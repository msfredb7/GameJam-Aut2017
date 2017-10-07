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
            if (IsInTransition())
                return stayNode;
            return transition.to;
        }
    }

    public PathOfDoom currentPath;
    public State state;

    public void GoToNode(Node destination)
    {
        //Meme chemin
        if (currentPath != null && destination == currentPath.GetDestination())
            return;

        if (state.IsInTransition())
        {
            if (destination == state.transition.to)
            {
                //On va deja a la bonne place, on arrete la
                Stop();
                return;
            }
            else if (destination == state.transition.from)
            {
                //On reviens sur nos pas et on arrete la
                state.transition.Flip();
                Stop();
                return;
            }
        }

        currentPath = Game.Fastar.CalculatePath(state.GetNextOrStayNode(), destination);

        if (state.IsInTransition())
        {
            //Shit ! Go back !
            if (currentPath.Get2ndClosest() == state.transition.from)
            {
                PerformNextSegment();
            }
        }
        else
        {
            PerformNextSegment();
        }
    }

    public void Stop()
    {
        currentPath = null;
    }

    public void PerformNextSegment()
    {
        //Delete l'ancienne transition si elle existe
        if (state.IsInTransition())
            state.transition.NoticeDelete();

        if (currentPath.nodes.Count == 1)
        {
            //On est arrivé !!
            Stop();
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
        }
    }
}
