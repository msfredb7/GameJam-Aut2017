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

        enabled = false;
        Game.OnGameReady += () =>
        {
            AfficheCash();
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
            if(minutes <= -1)
            {
                Game.instance.Lose();
            }
            else
            {
                seconds += 60;
            }
        }

        minutes = minutes.Raised(0);
        seconds = seconds.Raised(0);

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
            Game.instance.Win();

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
