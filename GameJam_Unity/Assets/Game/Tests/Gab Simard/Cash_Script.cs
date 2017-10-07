using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash_Script : MonoBehaviour {

    public float m_Cash, m_CashTarget;

    public Text m_CashDisplay;

    void Start()
    {
        m_Cash = 15000.0f;
        m_CashTarget = 30000.0f;
        m_CashDisplay.text = "Cash : " + m_Cash.ToString();
    }


    //  Depense d'argent du joueur
    //  dep : montant depenser
    public void OutcomeCash(float dep)
    {
        m_Cash -= dep;
        AfficheCash();
    }

    //  Revenus d'argent du joueur
    //  Rev : montant de revenus
    public void IncomeCash(float rev)
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
        if (m_Cash >= 0)
            m_CashDisplay.color = Color.black;
        else
            m_CashDisplay.color = Color.red;

        m_CashDisplay.text = "Cash : " + m_Cash.ToString();
    }
}
