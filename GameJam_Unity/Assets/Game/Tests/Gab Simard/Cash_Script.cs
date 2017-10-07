using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash_Script : MonoBehaviour {

    public int m_Cash, m_CashTarget;

    public Text m_CashDisplay;

    void Start()
    {
        AfficheCash();

        Game.OnGameReady += () => Game.GameUI.objectiveDisplay.SetObjectiveAmount(m_CashTarget);
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
}
