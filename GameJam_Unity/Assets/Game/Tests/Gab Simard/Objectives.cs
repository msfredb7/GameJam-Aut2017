using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour {

    public int m_Cash, m_CashTarget;

    public float minutes = 12;
    public float seconds = 30;

    void Start()
    {
        AfficheCash();

        enabled = false;
        Game.OnGameReady += () =>
        {
            enabled = true;
            Game.GameUI.objectiveDisplay.SetObjectiveAmount(m_CashTarget);
        };
    }

    void Update()
    {
        float dt = Time.deltaTime;

        seconds -= dt;

        if(seconds <= 0)
        {
            minutes--;
            seconds += 60;
        }
        AfficheTimer();
    }


    //  Depense d'argent du joueur
    //  dep : montant depenser
    public void OutcomeCash(int dep)
    {
        m_Cash -= dep;
        AfficheCash();
    }

    //  Revenus d'argent du joueur
    //  Rev : montant de revenus
    public void IncomeCash(int rev)
    {
        m_Cash += rev;
        AfficheCash();

        if (m_Cash >= m_CashTarget)
        {
            print("Ta gagner winner !!!!!!!");

            //Call fin de mission, objectif atteint
        }
    }

    //  Methode qui va set l'affiche de l'UI du montant d'argent
    public void AfficheCash()
    {
        Game.GameUI.objectiveDisplay.SetCashAmount(m_Cash);
    }


    public void AfficheTimer()
    {
        Game.GameUI.objectiveDisplay.SetObjectiveDuration(minutes, seconds);
    }
}
