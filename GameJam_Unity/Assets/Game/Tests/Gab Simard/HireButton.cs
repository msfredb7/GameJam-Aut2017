using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Manager;

public class HireButton : MonoBehaviour {

    public HeroShop_Script ScriptHire;
    public bool m_HireHide;
    public AudioClip sfx_click;

    void Start()
    {
        m_HireHide = false;
    }

    //  Afficher ou cacher la liste des héros disponible a l'embauche
    public void toggleHire()
    {
        SoundManager.PlayStaticSFX(sfx_click);

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
