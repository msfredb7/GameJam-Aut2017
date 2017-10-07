using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GabSimard_TestScript : MonoBehaviour {

    public float m_Cash, m_CashTarget;

    public Text m_CashDisplay;

    // Use this for initialization
    void Start()
    {
        m_Cash = 15000.0f;
        m_CashTarget = 30000.0f;
        m_CashDisplay.text = "Cash : " + m_Cash.ToString() + "\r\n Objectif : " + m_CashTarget.ToString();

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
            m_CashTarget += 30000;
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

        m_CashDisplay.text = "Cash : " + m_Cash.ToString() + "\r\n Objectif : " + m_CashTarget.ToString();
    }
}
