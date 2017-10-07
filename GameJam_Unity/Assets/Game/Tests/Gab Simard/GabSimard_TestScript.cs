using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GabSimard_TestScript : MonoBehaviour {

    public HeroShop_Script ScriptHire;
    public Button m_btnHire;
    public GameObject PannelInfo;
    public bool m_HireHide;

    void Start()
    {
        m_HireHide = false;
    }


    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            ScriptHire.showList();
        else if (Input.GetKeyDown(KeyCode.Y))
            ScriptHire.hideList();
    }*/

    //  Afficher ou cacher la liste des héros disponible a l'embauche
    public void toggleHire()
    {
        if(m_HireHide == false)
        {
            m_HireHide = true;
            //m_btnHire.image 

            ScriptHire.showList();
        }
        else
        {
            m_HireHide = false;
            ScriptHire.hideList();
        }
    }


    public void ShowHireInfo()
    {
        RectTransform pos = GetComponent<RectTransform>();
        Instantiate(PannelInfo, pos, false);

    }
}
