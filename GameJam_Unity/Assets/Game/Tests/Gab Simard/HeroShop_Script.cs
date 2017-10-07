using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeroShop_Script : MonoBehaviour {

    public List<ShopHeroIcon> listHero = new List<ShopHeroIcon>();
    public Vector2 m_HidenPos, m_VisiblePos;

    public void showList()
    {
        RectTransform mashit = GetComponent<RectTransform>();
        mashit.DOAnchorPos(m_VisiblePos, 0.5f).SetEase(Ease.OutSine);
    }

    public void hideList()
    {
        RectTransform mashit = GetComponent<RectTransform>();
        mashit.DOAnchorPos(m_HidenPos, 0.5f).SetEase(Ease.OutSine);
    }
}
