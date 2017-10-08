using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HBD_Button : MonoBehaviour
{
    [Header("Buttons")]
    public Sprite blueButton;
    public Sprite redButton;
    public Sprite yellowButton;
    public Sprite whiteButton;
    public Sprite greenButton;

    [Header("Link")]
    public Image buttonImage;

    public void SetColor(HeroActions.NodeColor color)
    {
        Sprite spr = null;
        switch (color)
        {
            case HeroActions.NodeColor.Red:
                spr = redButton;
                break;
            case HeroActions.NodeColor.Yellow:
                spr = yellowButton;
                break;
            case HeroActions.NodeColor.Green:
                spr = greenButton;
                break;
            case HeroActions.NodeColor.White:
                spr = whiteButton;
                break;
            case HeroActions.NodeColor.Blue:
                spr = blueButton;
                break;
            default:
                break;
        }
        buttonImage.sprite = spr;
    }
}
