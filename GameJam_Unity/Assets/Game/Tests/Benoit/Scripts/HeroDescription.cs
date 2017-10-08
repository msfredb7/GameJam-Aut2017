using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "HeroCity/HeroDescription")]
public class HeroDescription : ScriptableObject
{
    public Sprite heroFace;
    public Sprite heroBody;
    public string heroDescription;
    public int price;
}
