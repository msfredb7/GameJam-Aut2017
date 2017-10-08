using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireButton : MonoBehaviour {

    public HeroShop_Script ScriptHire;

    public void toggleHire()
    {
        ScriptHire.ToggleVisibility();
    }
}
