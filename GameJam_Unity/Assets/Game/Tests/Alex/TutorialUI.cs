using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class TutorialUI : MonoBehaviour {

    public GameBootUp gameBootUp;
    public Spotlight spotlight;

    public RectTransform heroPanel;

	void Start ()
    {
        gameBootUp.onGameBooted += ShowHeroSelected;
    }

    void ShowHeroSelected()
    {
        spotlight.On()
        DelayManager.LocalCallTo()

    }

    void ShowCashTimeObjective()
    {

    }

    void ShowHireHeroeButton()
    {

    }
}
