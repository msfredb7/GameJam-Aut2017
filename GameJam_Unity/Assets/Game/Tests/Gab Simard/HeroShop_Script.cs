using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeroShop_Script : MonoBehaviour {

    public List<ShopHeroIcon> listHero = new List<ShopHeroIcon>();
    public Vector2 m_HidenPos, m_VisiblePos;
    public float showDuration = 0.35f;

    public bool isVisible = false;

    public void showList()
    {
        isVisible = true;
        RectTransform malist = GetComponent<RectTransform>();
        malist.DOAnchorPos(m_VisiblePos, showDuration).SetEase(Ease.OutSine);
    }

    public void hideList()
    {
        isVisible = false;
        RectTransform malist = GetComponent<RectTransform>();
        malist.DOAnchorPos(m_HidenPos, showDuration).SetEase(Ease.InSine);
    }

    public void ToggleVisibility()
    {
        if (isVisible)
            hideList();
        else
            showList();
    }
}
