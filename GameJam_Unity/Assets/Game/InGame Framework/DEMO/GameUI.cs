using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public const string SCENENAME = "GameUI";

    public Action onHeroSelected;

    public HeroShop heroShop;
    public ObjectiveDisplay objectiveDisplay;

    public Texture2D cursorSprite;

    public HeroPortrait portrait;

    void Start()
    {
        if(cursorSprite != null)
            Cursor.SetCursor(cursorSprite, new Vector2(0, 0), CursorMode.Auto);
    }
}
