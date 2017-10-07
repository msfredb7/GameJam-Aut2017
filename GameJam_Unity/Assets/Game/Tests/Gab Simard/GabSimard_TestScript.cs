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

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

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
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
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
