using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Manager;

public class HireButton : MonoBehaviour {

    public HeroShop_Script ScriptHire;
    public AudioClip sfx_click;

    //  Afficher ou cacher la liste des héros disponible a l'embauche
    public void toggleHire()
    {
        SoundManager.PlayStaticSFX(sfx_click);
        ScriptHire.ToggleVisibility();
    }
}
