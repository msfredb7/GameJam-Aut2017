using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireButton : MonoBehaviour {

    public HeroShop_Script ScriptHire;
    public bool m_HireHide;

    void Start()
    {
        m_HireHide = false;
    }

    //  Afficher ou cacher la liste des héros disponible a l'embauche
    public void toggleHire()
    {
        if (m_HireHide == false)
        {
            m_HireHide = true;
            ScriptHire.showList();
        }
        else
        {
            m_HireHide = false;
            ScriptHire.hideList();
        }
    }
}
